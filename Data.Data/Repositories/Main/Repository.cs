using ELI.Data.Context;
using ELI.Domain.Contracts.Main;
using ELI.Entity.Main;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using ELI.Domain.Helpers;
using ELI.Domain.ViewModels;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace ELI.Data.Repositories.Main
{
    public class ELIRepository : IELIRepository
    {
        string ConString;

        private readonly ELIContext _context;
        public ELIRepository(ELIContext context)
        {
            _context = context;
            var builder = new ConfigurationBuilder()
           .AddJsonFile("appsettings.json")
           .AddJsonFile($"appsettings.json", true)
           .AddEnvironmentVariables();
            var config = builder.Build();
            ConString = config.GetConnectionString("ELIDb");

        }
        public void Dispose()
        {
            _context.Dispose();
        }
        public async Task<List<Countries>> GetAllCountriesAsync(CancellationToken ct = default(CancellationToken))
        {
            return await _context.Countries.ToListAsync(ct);
        }
        public async Task<List<Region>> GetAllRegionAsync(CancellationToken ct = default(CancellationToken))
        {
            var regions = await _context.Region.Where(a => a.IsActive == true && a.IsDeleted == false).ToListAsync(ct);
            foreach (var region in regions)
            {
                region.Password = EncodePasswordToBase64(region.Password);

            }
            return regions;
        }
        public async Task<List<ContactInfo>> GetContactInformationAsync(CancellationToken ct = default(CancellationToken))
        {
            return await _context.ContactInfo.ToListAsync(ct);
           
        }
        public async Task<Region> GetRegionById(int regionId, CancellationToken ct = default(CancellationToken))
        {
            return await _context.Region.Where(a => a.IsActive == true && a.IsDeleted == false && a.RegionId == regionId).FirstOrDefaultAsync();
        }
        public async Task<List<Cities>> GetAllCitiesAsync(CancellationToken ct = default(CancellationToken))
        {
            return await _context.Cities.ToListAsync(ct);
        }
        public async Task<List<Cities>> GetCitiesByCountryAsync(string countryName, CancellationToken ct = default(CancellationToken))
        {
            List<Cities> CityList = new List<Cities>();
            var states = await _context.States.Where(a => a.Country.Name == countryName).ToListAsync();
            foreach (var state in states)
            {
                var cities = _context.Cities.Where(a => a.StateId == state.Id).ToList();
                foreach (var city in cities)
                {
                    city.State = null;
                    CityList.Add(city);
                }
            }
            return CityList;
        }
        public async Task<List<States>> GetAllStatesAsync(int countryId, CancellationToken ct = default(CancellationToken))
        {
            return await _context.States.Where(a => a.CountryId == countryId).ToListAsync(ct);
        }
        public async Task<Pricing> CreatePricingAsync(Pricing pricing, CancellationToken ct = default(CancellationToken))
        {
            await _context.Pricing.AddAsync(pricing, ct);
            await _context.SaveChangesAsync(ct);
            return pricing;
        }
        public async Task<ShowPricing> CreateShowPricingAsync(ShowPricing showPricing, CancellationToken ct = default(CancellationToken))
        {
            await _context.ShowPricing.AddAsync(showPricing, ct);
            await _context.SaveChangesAsync(ct);
            return showPricing;
        }
        public async Task<ErrorLogging> CreateLogAsync(ErrorLogging log, CancellationToken ct = default(CancellationToken))
        {
            await _context.ErrorLogging.AddAsync(log, ct);
            await _context.SaveChangesAsync(ct);
            return log;
        }
        public async Task<List<Currency>> GetCurrency(CancellationToken ct = default(CancellationToken))
        {
            return await _context.Currency.ToListAsync(ct);
        }
        public static string EncodePasswordToBase64(string password)
        {
            try
            {
                byte[] encData_byte = new byte[password.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new AppException("Error in base64Encode" + ex.Message);
            }
        }
        public string DecodeFrom64(string encodedData)
        {
            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            System.Text.Decoder utf8Decode = encoder.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(encodedData);
            int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            string result = new String(decoded_char);
            return result;
        }
        public async Task<Pricing> GetPricingByShowIdAsync(int showId, CancellationToken ct = default(CancellationToken))
        {
            var showPricing = await _context.ShowPricing.SingleOrDefaultAsync(a => a.ShowId == showId && a.IsDeleted == false);
            var pricing = await _context.Pricing.SingleOrDefaultAsync(a => a.PricingId == showPricing.PricingId && a.IsDeleted == false);
            if (pricing != null)
            {
                return pricing;
            }
            else
                return null;
        }
        public async Task UpdatePricingAsync(Pricing pricing, CancellationToken ct = default(CancellationToken))
        {
            var pricingtoUpdated = await _context.Pricing.SingleOrDefaultAsync(a => a.PricingId == pricing.PricingId && a.IsDeleted == false);

            if (pricingtoUpdated == null) throw new AppException("Show not found");

            pricingtoUpdated.KeyAmount = pricing.KeyAmount;
            pricingtoUpdated.TaxName = pricing.TaxName;
            pricingtoUpdated.Tax = pricing.Tax;
            _context.Pricing.Update(pricingtoUpdated);
            await _context.SaveChangesAsync();
        }
        public async Task<ShowDiscount> CreateShowDiscountAsync(ShowDiscount showDiscount, CancellationToken ct = default(CancellationToken))
        {
            await _context.ShowDiscount.AddAsync(showDiscount, ct);
            await _context.SaveChangesAsync(ct);
            return showDiscount;
        }
        public async Task<List<Discount>> GetShowDiscountByShowIdAsync(int ShowId, CancellationToken ct = default(CancellationToken))
        {
            List<Discount> codes = new List<Discount>();
            var showDiscounts = await _context.ShowDiscount.Where(a => a.ShowId == ShowId).ToListAsync();
            if(showDiscounts.Count!=0)
            {
                foreach (var item in showDiscounts)
                {
                    var code = await _context.Discount.SingleOrDefaultAsync(a => a.DiscountId == item.DiscountId);
                    codes.Add(code);
                }
            }
            return codes;
        }
        public async Task<List<ShowDiscount>> DeleteShowDiscountsRelationsAsync(int ShowId, CancellationToken ct = default(CancellationToken))
        {
             var ShowDiscounts = await _context.ShowDiscount.Where(a => a.ShowId == ShowId).ToListAsync();
            _context.ShowDiscount.RemoveRange(ShowDiscounts);
            await _context.SaveChangesAsync();
            foreach (var item in ShowDiscounts)
            {
                var code = await _context.Discount.SingleOrDefaultAsync(a => a.DiscountId == item.DiscountId && a.IsConsumed==true);
                code.IsConsumed = false;
                _context.Discount.Update(code);
                await _context.SaveChangesAsync();
            }
            return ShowDiscounts;
        }
        public bool ValidateDeviceIdentifier(string deviceIdentifier)
        {
            var device = (from d in _context.Device
                          where d.IsDeleted == false && d.DeviceIdentifier == deviceIdentifier
                          select d
                        ).ToList();

            if(device != null && device.Count > 0)
            {
                return true;
            }

            return  false;
        }
        public async Task<string> AUSSuccessCase(int invoiceId, string responseCode)
        {
            var invoice = (from d in _context.Invoice
                           where d.IsDeleted == false && d.InvoiceId == invoiceId && d.IsSuccess == false
                           select d
                        ).FirstOrDefault();

            if (invoice != null)
            {

                invoice.ResponseXml = "success" + "Response Code is : " + responseCode;
                invoice.IsSuccess = true;
                _context.Invoice.Update(invoice);
                await _context.SaveChangesAsync();
                GenerateKeys(invoice.ShowId != null ? (int)invoice.ShowId : -99, invoice.UserId != null ? (int)invoice.UserId : -99, invoice.Quantity != null ? (int)invoice.Quantity : -99, 7, invoice.InvoiceId);
                var qualifiers = await _context.Qualifier.Where(a => a.ShowId == invoice.ShowId && a.IsPublished == true && a.IsAdmin == true).ToListAsync();
                if (qualifiers != null)
                {
                    foreach (var item in qualifiers)
                    {
                        var UserQualifiers = await _context.QualifierUsers.Where(a => a.UserId == invoice.UserId && a.QualifierId == item.QualifierId).ToListAsync();
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
                return invoice.PaymentId;
            }

            return "";
        }

        public string GenerateKeys(int showId, int userId, int Quantity, int activationTypeId, int? invoiceId)
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
                throw new AppException("Keys Not generated" + ex.Message);
            }
        }

        public async Task<string> AUSFailCase(int invoiceId, string responseCode)
        {
            var invoice = (from d in _context.Invoice
                           where d.IsDeleted == false && d.InvoiceId == invoiceId && d.IsSuccess == false
                           select d
                        ).FirstOrDefault();

            if (invoice != null)
            {

                invoice.ResponseXml = "Fail - " + "Response Code is : " + responseCode;
                invoice.IsSuccess = false;
                _context.Invoice.Update(invoice);
                await _context.SaveChangesAsync();
                return invoice.PaymentId;
            }

            return "";
        }


    }
}
