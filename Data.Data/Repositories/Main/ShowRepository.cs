using ELI.Data.Context;
using ELI.Domain.Contracts;
using ELI.Domain.Contracts.Main;
using ELI.Entity;
using ELI.Entity.Main;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using ELI.Domain.Helpers;
using System.Data;
using System.Data.SqlClient;
using ELI.Domain.ViewModels;
using AutoMapper;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.Configuration;
using ELI.Domain.Services;

namespace ELI.Data.Repositories.Main
{
    public class ShowRepository : IShowRepository
    {
        private readonly ELIContext _context;
        private readonly IMapper _mapper;
        string ConString;
        string ImgPath;
        public ShowRepository(ELIContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            var builder = new ConfigurationBuilder()
           .AddJsonFile("appsettings.json")
           .AddJsonFile($"appsettings.json", true)
           .AddEnvironmentVariables();
            var config = builder.Build();
            ConString = config.GetConnectionString("ELIDb");
            ImgPath = config.GetSection("ConnectionStrings").GetSection("ImagePathKeyServe").Value;
        }
        public void Dispose()
        {
            _context.Dispose();
        }
        public async Task<Show> CreateShowAsync(Show show, CancellationToken ct = default(CancellationToken))
        {
            await _context.Show.AddAsync(show, ct);
            await _context.SaveChangesAsync(ct);

            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection sqlConn = new SqlConnection(ConString))
                {
                    string sql = "spCopyDefaultQualifier";
                    using (SqlCommand sqlCmd = new SqlCommand(sql, sqlConn))
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.AddWithValue("@showid", show.ShowId);
                        sqlCmd.Parameters.AddWithValue("@userid", show.CreatedBy);
                        sqlConn.Open();
                        using (SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCmd))
                        {
                            sqlAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                new ExceptionHandlingService(ex, null, null).LogException();
                return BadRequest(new { message = ex.Message });
            }


            return show;
        }

        private Show BadRequest(object p)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ShowByRegionViewModel>> GetAllShowsByRegion(int regionId, string DeviceIdentifier, CancellationToken ct = default(CancellationToken))
        {
            var action = await DeleteOldShowsAsync();
            var deviceId = 0;
            List<ShowByRegionViewModel> ShowByRegionViewModel = new List<ShowByRegionViewModel>();
            var device = _context.Device.FirstOrDefault(a => a.DeviceIdentifier == DeviceIdentifier && a.IsDeleted == false && a.IsActive == true);
            if (device != null)
            {
                deviceId = device.DeviceId;
            }
            var builder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddJsonFile($"appsettings.json", true)
            .AddEnvironmentVariables();
            var config = builder.Build();
            int delay = Convert.ToInt32(config.GetConnectionString("AddDelay"));
            var show = (from s in _context.Show
                        join sdu in _context.Sduactivation on s.ShowId equals sdu.ShowId
                        join a in _context.Activation on sdu.ActivationId equals a.ActivationId
                        where
                       //s.IsActive == true &&
                       s.EndDate.Value.AddDays(delay+1) > DateTime.Now &&
                       s.IsDeleted == false &&
                        sdu.DeviceId == deviceId &&
                        s.RegionId == regionId && a.IsActive == true
                        orderby sdu.ActivationTime

                        select new ShowByRegionViewModel
                        {
                            ShowId = s.ShowId,
                            ShowKey = s.ShowKey,
                            ShowCode = s.ShowCode,
                            ShowName = s.ShowName,
                            RegionId = s.RegionId,
                            Location = s.Location,
                            StartDate = s.StartDate,
                            EndDate = s.EndDate,
                            Description = s.Description,
                            DatabaseId = s.DatabaseId,
                            PreRegCode = s.PreRegCode,
                            Message = s.Message,
                            BarcodePrefix = s.BarcodePrefix,
                            IsRestricted = s.IsRestricted,
                            OnsiteHelpNumber = s.OnsiteHelpNumber,
                            CreatedDate = s.CreatedDate,
                            PaymentGatewayId = s.PaymentGatewayId,
                            Logo = s.Logo ,
                            ImagePath = ImgPath,
                            ShowShortName = s.ShowShortName,
                            IsNfc = s.IsNfc,
                            LeadsDownloadLimit = s.LeadsDownloadLimit,
                            LeadsSequentialLimit = s.LeadsSequentialLimit,
                            IsActive = s.IsActive,
                            ActivationTime = (DateTime?)sdu.ActivationTime,
                            ActivationId = (int?)sdu.ActivationId,
                            ActivationKey = a.ActivationKey,
                            Company = sdu.Company,
                            DeviceId = (int?)sdu.DeviceId,
                            Unlocked = false,
                            IsConsumed = (bool?)sdu.IsConsumed,
                            Name = sdu.Name,
                            StandNumber = sdu.StandNumber,
                            UserId = (int?)sdu.UserId,
                            SDUActivationId = (int?)sdu.SduactivationId
                        }).ToList();
            return show;
        }
        public async Task<List<Show>> GetAllAsync(CancellationToken ct = default(CancellationToken))
        {
            // var t = await _context.Show.Select(x=> new Show { ShowId = x.ShowId, ShowName= x.ShowName}).ToListAsync(ct);
            var shows = await _context.Show.Where(a => a.IsDeleted == false && a.IsActive == true).ToListAsync(ct);
            foreach (var s in shows)
            {
                s.ImagePath = ImgPath;
            }
            return shows;
        }
        public async Task<List<Show>> GetAllRAsync(CancellationToken ct = default(CancellationToken))
        {
            // var t = await _context.Show.Select(x=> new Show { ShowId = x.ShowId, ShowName= x.ShowName}).ToListAsync(ct);
            var shows = await _context.Show.Where(a => a.IsDeleted == false ).ToListAsync(ct);
            foreach (var s in shows)
            {
                s.ImagePath = ImgPath;
            }
            return shows;
        }
        public async Task<List<Show>> GetAllShowsByUserIdAsync(string role, int userid, bool isDashboard, CancellationToken ct = default(CancellationToken))
        {
            var action = await DeleteOldShowsAsync();
            if (isDashboard == true)
            {
                if (role == "Admin")
                {
                    return await _context.Show.Where(a => a.IsActive == true && a.IsActive != null && a.IsDeleted == false)
                        .Select(x => new Show
                        {
                            ShowId = x.ShowId,
                            ShowName = x.ShowName,
                            Location = x.Location,
                            StartDate = x.StartDate,
                            EndDate = x.EndDate,
                            IsActive = x.IsActive,
                            PaymentGatewayId = x.PaymentGatewayId
                        })//.OrderBy(a => a.EndDate > DateTime.Now ? 0 : 1).ThenBy(a => a.EndDate)
                        .ToListAsync(ct);
                }
                else
                {
                    var show = (from s in _context.Show
                                join sdu in _context.Sduactivation on s.ShowId equals sdu.ShowId into t
                                from subpet in t.DefaultIfEmpty()
                                where s.IsActive == true && s.IsActive != null && s.IsDeleted == false && (subpet.UserId == userid)
                                //orderby s.EndDate > DateTime.Now ? 0 : 1
                                //orderby s.EndDate
                                //orderby s.IsActive

                                select new Show
                                {
                                    ShowId = s.ShowId,
                                    ShowName = s.ShowName,
                                    Location = s.Location,
                                    StartDate = s.StartDate,
                                    EndDate = s.EndDate,
                                    IsActive = s.IsActive,
                                    PaymentGatewayId = s.PaymentGatewayId
                                }).Distinct().ToList();

                    return show;
                }
            }
            else
            {
                if (role == "Admin")
                {
                    var show = (from s in _context.Show
                                where s.IsDeleted == false
                                orderby s.IsActive descending,   
                                 s.StartDate descending
                                select new Show
                                {
                                    ShowId = s.ShowId,
                                    ShowName = s.ShowName,
                                    Location = s.Location,
                                    StartDate = s.StartDate,
                                    EndDate = s.EndDate,
                                    IsActive = s.IsActive,
                                    PaymentGatewayId = s.PaymentGatewayId
                                }).Distinct().ToList();
                    return show;

                    //return await _context.Show.Where(a => a.IsActive != null && a.IsDeleted == false).Select(x => new Show
                    //{
                    //    ShowId = x.ShowId,
                    //    ShowName = x.ShowName,
                    //    Location = x.Location,
                    //    StartDate = x.StartDate,
                    //    EndDate = x.EndDate,
                    //    IsActive = x.IsActive,
                    //    PaymentGatewayId = x.PaymentGatewayId
                    //})//.OrderBy(a => a.EndDate > DateTime.Now ? 0 : 1).ThenBy(a => a.EndDate)
                    //.ToListAsync(ct);
                }
                else
                {
                    var show = (from s in _context.Show
                                join sdu in _context.Sduactivation on s.ShowId equals sdu.ShowId into t
                                from subpet in t.DefaultIfEmpty()
                                where s.IsDeleted == false && (subpet.UserId == userid)
                                //orderby s.EndDate > DateTime.Now ? 0 : 1
                                //orderby s.EndDate
                                //orderby s.IsActive
                                select new Show
                                {
                                    ShowId = s.ShowId,
                                    ShowName = s.ShowName,
                                    Location = s.Location,
                                    StartDate = s.StartDate,
                                    EndDate = s.EndDate,
                                    IsActive = s.IsActive,
                                    PaymentGatewayId = s.PaymentGatewayId
                                }).Distinct().ToList();
                 
                    return show;
                }
            }
        }
        public async Task<List<Show>> GetLimitednumberShowsAsync(int key, int length, CancellationToken ct = default(CancellationToken))
        {
            var a = await _context.Show.Where(b => b.ShowId > key).Take(length).ToListAsync(ct);
            return a;
        }
        public bool CheckShowKeyDuplication(string showKey, CancellationToken ct = default(CancellationToken))
        {
            if (_context.Show.Any(x => x.ShowKey == showKey)) // && x.IsActive == true && x.IsDeleted == false : MM- we do not need to check
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<ShowViewModel> GetByIdAsync(int id, CancellationToken ct = default(CancellationToken))
        {
            ShowViewModel showViewModel = new ShowViewModel();
            var Showpricing = await _context.ShowPricing.FirstOrDefaultAsync(a => a.ShowId == id && a.IsDeleted == false);
            var pricing = await _context.Pricing.FirstOrDefaultAsync(a => a.PricingId == Showpricing.PricingId && a.IsDeleted == false);
            var show = await _context.Show.FirstOrDefaultAsync(a => a.ShowId == id && a.IsDeleted == false);
            showViewModel = _mapper.Map<ShowViewModel>(show);
            showViewModel.TaxName = pricing.TaxName;
            showViewModel.Tax = pricing.Tax;
            showViewModel.KeyAmount = pricing.KeyAmount;
            showViewModel.Logo = ImgPath + showViewModel.Logo;
            showViewModel.ImagePath = ImgPath;
            return showViewModel;
        }
        public async Task<List<Show>> DeleteOldShowsAsync(CancellationToken ct = default(CancellationToken))
        {
            var showstobeInactive = await _context.Show.Where(a => a.EndDate.Value.AddDays(1) < DateTime.Now && a.IsActive == true && a.IsDeleted == false).ToListAsync(ct);
            foreach (var show in showstobeInactive)
            {
                show.IsActive = false;
                _context.Show.Update(show);
            }
            await _context.SaveChangesAsync();
            return showstobeInactive;
        }
        public async Task UpdateShowAsync(Show showViewModel, CancellationToken ct = default(CancellationToken))
        {
            var show = await _context.Show.SingleOrDefaultAsync(a => a.ShowId == showViewModel.ShowId && a.IsDeleted == false);
            if (show == null) throw new AppException("Show not found");
            show.ShowName = showViewModel.ShowName;
            show.StartDate = showViewModel.StartDate;
            show.ShowShortName = showViewModel.ShowShortName;
            show.ShowKey = showViewModel.ShowKey;
            show.PaymentGatewayId = showViewModel.PaymentGatewayId;
            show.OnsiteHelpNumber = showViewModel.OnsiteHelpNumber;
            show.Location = showViewModel.Location;
            show.LeadsSequentialLimit = showViewModel.LeadsSequentialLimit;
            show.LeadsDownloadLimit = showViewModel.LeadsDownloadLimit;
            show.IsRestricted = showViewModel.IsRestricted;
            show.IsNfc = showViewModel.IsNfc;
            show.EndDate = showViewModel.EndDate;
            show.Description = showViewModel.Description;
            show.DatabaseId = showViewModel.DatabaseId;
            show.DbName = showViewModel.DbName;
            //show.DiscountId = showViewModel.DiscountId;
            show.Logo = showViewModel.Logo;
            show.UpdatedDate = DateTime.Now;
            show.BarcodePrefix = showViewModel.BarcodePrefix;
            show.UpdatedBy = showViewModel.UpdatedBy;
            show.IsActive = showViewModel.IsActive;
            _context.Show.Update(show);
            await _context.SaveChangesAsync();
        }
        public async Task<List<Show>> GetShowsforPurchaseActivationAsync(CancellationToken ct = default(CancellationToken))
        {
            var shows = await _context.Show.Include(a => a.ShowPricing).Include("ShowPricing.Pricing").Where(a => a.EndDate.Value.AddDays(1) > DateTime.Now && a.EndDate != null && a.IsActive == true && a.IsDeleted == false).ToListAsync(ct); //.Select(x => new Show { ShowId = x.ShowId, ShowName = x.ShowName })
            foreach (var sh in shows)
            {
                sh.ImagePath = ImgPath;
            }
            return shows;
        }
        public async Task<Discount> ValidateDiscountCode(string code, int showId, CancellationToken ct = default(CancellationToken))
        {
            var discountCode = await _context.Discount.Where(a => a.DiscountCode == code && a.IsActive == true && a.IsDeleted == false && a.ExpirationDate >= DateTime.Now).FirstOrDefaultAsync();
            if (discountCode != null)
            {
                var Discountshow = await _context.ShowDiscount.Where(a => a.ShowId == showId && a.DiscountId == discountCode.DiscountId ).FirstOrDefaultAsync();
                if (Discountshow != null)
                {
                    return discountCode;
                }
                else
                {
                    throw new AppException("Discount Code not valid for this show.");

                }
            }
            else
            {
                throw new AppException("Invalid Discount code.");
            }
        }
        public async Task<string> GetRequestResponse(string PaymentId, CancellationToken ct = default(CancellationToken))
        {
            //We will make a GET request to a really cool website...

            string baseUrl = "https://cloudme02.infosalons.biz/webservices/isme-payments/API/NI/GetPaymentDetails?PaymentId=";
            baseUrl = baseUrl + PaymentId;
            using (HttpClient client = new HttpClient())
            using (HttpResponseMessage res = await client.GetAsync(baseUrl))
            using (HttpContent content = res.Content)
            {
                string data = await content.ReadAsStringAsync();
                if (data != null)
                {
                    var data1 = data;
                    if (data1.Contains("SUCCESS"))
                    {
                        var invoice = await _context.Invoice.SingleOrDefaultAsync(a => a.PaymentId == PaymentId && a.IsSuccess==false);
                        if (invoice != null)
                        {
                            invoice.ResponseXml = data;
                            invoice.IsSuccess = true;
                            _context.Invoice.Update(invoice);
                            await _context.SaveChangesAsync();
                            GenerateKeys(invoice.ShowId != null ? (int)invoice.ShowId : -99, invoice.UserId != null ? (int)invoice.UserId : -99, invoice.Quantity != null ? (int)invoice.Quantity : -99, 7, invoice.InvoiceId);
                            var qualifiers = await _context.Qualifier.Where(a => a.ShowId == invoice.ShowId && a.IsPublished == true &&a.IsAdmin==true).ToListAsync();
                            foreach (var item in qualifiers)
                            {
                                var UserQualifiers = await _context.QualifierUsers.Where(a => a.UserId == invoice.UserId &&a.QualifierId==item.QualifierId).ToListAsync();
                                if (UserQualifiers == null || (UserQualifiers != null && UserQualifiers.Count <= 0))
                                {
                                    QualifierUsers QU = new QualifierUsers();
                                    QU.QualifierId = item.QualifierId;
                                    QU.UserId = invoice.UserId;
                                    _context.QualifierUsers.Add(QU);
                                    await _context.SaveChangesAsync();
                                }
                            }
                        }
                        else
                        {
                            throw new AppException("Keys already generated against this invoice.");
                        }
                    }
                    return data;
                }
                else
                {
                    return "no response";
                }
            }
        }
        public string GenerateKeys(int showId, int userId, int Quantity, int activationTypeId,int? invoiceId)
        {
            try
            {
                EmailTemplateViewModel da = new EmailTemplateViewModel();
                DataTable dt = new DataTable();
                List<EmailTemplateViewModel> dal = new List<EmailTemplateViewModel>();
                using (SqlConnection sqlConn = new SqlConnection(ConString))
                {
                    string sql = "GenerateKey";
                    using (SqlCommand sqlCmd = new SqlCommand(sql, sqlConn))
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        //sqlCmd.Parameters.AddWithValue("@regionname,", RegionName);
                        sqlCmd.Parameters.Add("@quantity", SqlDbType.Int);
                        sqlCmd.Parameters["@quantity"].Value = Quantity;

                        sqlCmd.Parameters.Add("@showId", SqlDbType.Int);
                        sqlCmd.Parameters["@showId"].Value = showId;

                        sqlCmd.Parameters.Add("@userId", SqlDbType.Int);
                        sqlCmd.Parameters["@userId"].Value = userId;

                        sqlCmd.Parameters.Add("@ActivationTypeId", SqlDbType.Int);
                        sqlCmd.Parameters["@ActivationTypeId"].Value = activationTypeId;
                        if (invoiceId != null)
                        {
                            sqlCmd.Parameters.Add("@InvoiceId", SqlDbType.Int);
                            sqlCmd.Parameters["@InvoiceId"].Value = invoiceId;
                        }
                        else
                        {
                            sqlCmd.Parameters.Add("@InvoiceId", SqlDbType.Int);
                            sqlCmd.Parameters["@InvoiceId"].Value = DBNull.Value;
                        }

                       


                        sqlConn.Open();
                        using (SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCmd))
                        {
                            sqlAdapter.Fill(dt);
                        }
                    }
                }
                return "Keys Generated";
            }
            catch (Exception ex)
            {
                throw new AppException("Keys Not generated"+ ex.Message);
            }
        }
        public async Task<bool> ValidateRestrictedCode(string code, int showId, CancellationToken ct = default(CancellationToken))
        {
            var restrictedCode = await _context.Activation.Where(a => a.ActivationKey == code && a.IsActive == true && a.IsDeleted == false).FirstOrDefaultAsync();
            if (restrictedCode != null)
            {
                var verification = await _context.Sduactivation.Where(a => a.ShowId == showId && a.ActivationId== restrictedCode.ActivationId && a.IsDeleted == false && a.IsActive == true).FirstOrDefaultAsync();
                if (verification!=null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                throw new AppException("Invalid Restricted code.");
            }
        }
        public async Task<Show> GetShowByShowKey(string ShowKey, CancellationToken ct = default(CancellationToken))
        {
            var show = await _context.Show.SingleOrDefaultAsync(a => a.ShowKey == ShowKey && a.IsActive == true && a.IsDeleted == false);
            return show;
        }
        public async Task<Show> CheckShowActivationAsync(int id, CancellationToken ct = default(CancellationToken))
        {
            var show = await _context.Show.FirstOrDefaultAsync(a => a.ShowId == id && a.IsDeleted == false && a.IsActive==true);
            return show;
        }
        public async Task<ShowDiscount> GetShowDiscountRelationAsync(int ShowId, int DiscountId ,CancellationToken ct = default(CancellationToken))
        {
            var showDiscount = await _context.ShowDiscount.FirstOrDefaultAsync(a => a.ShowId == ShowId && a.DiscountId == DiscountId && a.IsDeleted == false && a.IsActive == true);
            return showDiscount;
        }
    }
}
