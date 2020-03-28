using ELI.Data.Context;
using ELI.Domain.Contracts.Main;
using ELI.Domain.Helpers;
using ELI.Domain.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Data;
using ELI.Data.Repositories.Main.Extensions;

namespace ELI.Data.Repositories.Main
{
    public class ReportRepository : BaseRepository, IReportRepository
    {
        private readonly ELIContext _context;
        public void Dispose()
        {
        }

        public ReportRepository(IConfiguration configuration) : base(configuration) { }
        private const string GetStoredProcedureName = "GetPaymentReportByYear";
        private const string YearParameterName = "PYear";
        private const string IDColumnName = "ID";
        private const string YearColumnName = "Year";
        private const string Reg_RefColumnName = "Reg_Ref";
        private const string FirstNameColumnName = "FirstName";
        private const string LastNameColumnName = "LastName";
        private const string CampusNameColumnName = "CampusName";
        private const string AgentNameColumnName = "AgentName";
        private const string FormatNameColumnName = "FormatName";
        private const string AgencyIDColumnName = "AgencyID";
        private const string CampusColumnName = "Campus";
        private const string FormatColumnName = "Format";
        private const string AgencyRefColumnName = "AgencyRef";
        private const string TotalGrossPriceColumnName = "TotalGrossPrice";
        private const string TotalGrossPriceCalculatedColumnName = "TotalGrossPriceCalculated";
        private const string TotalAddinsColumnName = "TotalAddins";
        private const string PaidColumnName = "Paid";
        private const string TotalPaidPriceCalculatedColumnName = "TotalPaidPriceCalculated";
        private const string CommisionColumnName = "Commision";
        private const string CommissionAddinsColumnName = "CommissionAddins";
        private const string NetPriceColumnName = "NetPrice";
        private const string TotalNetPriceCalculatedColumnName = "TotalNetPriceCalculated";
        private const string BalanceColumnName = "Balance";
        private const string TotalBalanceCalculatedColumnName = "TotalBalanceCalculated";
        private const string ProgramNameColumnName = "ProgramName";
        private const string SubProgramNameColumnName = "SubProgramName";
        public async Task<List<LeadsCountViewModel>> LeadsCountReportAsync(String showkey, CancellationToken ct = default(CancellationToken))
        {
            var show = await _context.Show.SingleOrDefaultAsync(a => a.ShowKey == showkey);
            if (show != null)
            {
                List<LeadsCountViewModel> LCVMList = new List<LeadsCountViewModel>();
                var sdus = await _context.Sduactivation.Include(a=>a.User).Include(a=>a.Activation).Where(a => a.ShowId == show.ShowId && a.DeviceId != null && a.IsDeleted==false).Distinct().ToListAsync();
                foreach (var item in sdus)
                {
                    var leads = await _context.Leads.Where(a => a.Sduid == item.SduactivationId).ToListAsync();
                    LeadsCountViewModel LCVM = new LeadsCountViewModel();
                    LCVM.AmountOfLeads = leads.Count();
                    var device = await _context.Device.SingleOrDefaultAsync(a => a.DeviceId == item.DeviceId && a.IsDeleted==false);
                    LCVM.DeviceIdentifier = device.DeviceIdentifier;
                    LCVM.StandNumber = item.StandNumber;
                    LCVM.Email = item.User.Email;
                    LCVM.FirstName = item.User.FirstName + item.User.SurName;
                    LCVM.ActivationKey = item.Activation.ActivationKey;
                    LCVM.Company = item.Company;
                    LCVM.ShowName = _context.Show.Where(a => a.ShowId == item.ShowId).FirstOrDefault().ShowName;
                    LCVMList.Add(LCVM);
                }
                //          var leadsCount = (from leads in _context.Leads
                //                            join
                //sdu in _context.Sduactivation on leads.Sduid equals sdu.SduactivationId
                //                            where sdu.ShowId == show.ShowId && sdu.DeviceId!=null
                //                           // select leads
                //                            select new LeadsCountViewModel
                //                            {
                //                                DeviceIdentifier = sdu.DeviceId.ToString(),
                //                                AmountOfLeads = ,



                //                            }
                //                            ).ToList();

                return LCVMList;
            }
            else
            {
                throw new AppException("ShowKey not correct");
            }
        }
        public async Task<List<AccountListReportViewModel>> AccountListReportAsync(CancellationToken ct = default(CancellationToken))
        {


            var accountList = await (from invoice in _context.Invoice.Include(a => a.Show).Include(a => a.user).Include("Show.ShowPricing.Pricing")
                                     join
                                     act in _context.Activation on invoice.InvoiceId equals act.InvoiceId
                                     join
                                     sdu in _context.Sduactivation on act.ActivationId equals sdu.ActivationId
                                     where act.InvoiceId == invoice.InvoiceId &&
                                     sdu.UserId == invoice.UserId && 
                                     invoice.IsDeleted == false &&
                                     act.IsDeleted == false &&
                                     sdu.IsDeleted == false


                                     select new AccountListReportViewModel
                                     {
                                         InvoiceId = invoice.InvoiceId,
                                         FirstName = invoice.user.FirstName,
                                         SurName = invoice.user.SurName,
                                         Company = invoice.user.Company,
                                         StreetAddress = invoice.user.StreetAddress,
                                         Suburb = invoice.user.Suburb,
                                         State_Province = invoice.user.State_Province,
                                         PostalCode = invoice.user.PostalCode,
                                         Country = invoice.user.Country,
                                         PhoneNumber = invoice.user.PhoneNumber,
                                         Email = invoice.user.Email,
                                         Purchased = invoice.user.CreatedDate,
                                         Total = invoice.Total,
                                         Quantity = invoice.Quantity,
                                         Show = invoice.Show.ShowName
                                     }
                              ).ToListAsync();

            foreach (var item in accountList)
            {
                int value;
                if (int.TryParse(item.Country, out value))
                {
                    var country = await _context.Countries.SingleOrDefaultAsync(a => a.Id == Convert.ToInt32(item.Country));
                    item.Country = country.Name;
                }

                if (int.TryParse(item.State_Province, out value))
                {
                    var state = await _context.States.SingleOrDefaultAsync(a => a.Id == Convert.ToInt32(item.State_Province));
                    item.State_Province = state.Name;
                }
                var keys = await _context.Activation.Where(a => a.InvoiceId == item.InvoiceId && a.IsDeleted==false).ToListAsync();
                foreach (var key in keys)
                {
                    var sdu = await _context.Sduactivation.SingleOrDefaultAsync(a => a.ActivationId == key.ActivationId && a.IsDeleted==false);
                    if (sdu != null)
                    {
                        var leads = await _context.Leads.Where(a => a.Sduid == sdu.SduactivationId).ToListAsync();
                        if (leads != null && leads.Count != 0)
                        {
                            item.TotalLeads = item.TotalLeads + leads.Count();
                        }
                        if (sdu.IsConsumed == true)
                        {
                            item.UsedCodes = item.UsedCodes + 1;
                        }
                        else if (sdu.IsConsumed == false)
                        {
                            item.UnusedCodes = item.UnusedCodes + 1;
                        }
                    }
                }
                item.TotalCodes = item.UsedCodes + item.UnusedCodes;
            }

            return accountList;
        }
        public async Task<List<CodeListReportViewModel>> CodeListReportAsync(CancellationToken ct = default(CancellationToken))
        {
            var keysList = await (from keys in _context.Activation.Include(d => d.Invoice)
                                  join
                                  sdu in _context.Sduactivation.Include(a => a.User) on keys.ActivationId equals sdu.ActivationId
                                  join sh in _context.Show on sdu.ShowId equals sh.ShowId
                                  where keys.ActivationTypeId == 7 &&
                                  keys.IsDeleted == false &&
                                  sdu.IsDeleted == false &&
                                  sh.IsDeleted == false

                                  select new CodeListReportViewModel
                                  {
                                      State_Province = sdu.User.State_Province,
                                      PostalCode = sdu.User.PostalCode,
                                      Country = sdu.User.Country,
                                      Total = keys.Invoice.Total,
                                      PhoneNumber = sdu.User.PhoneNumber,
                                      Email = sdu.User.Email,
                                      Purchased = sdu.CreatedDate,
                                      Code = keys.ActivationKey,
                                      Used = sdu.IsConsumed,
                                      ShowName = sh.ShowName,
                                      KeyPrice = keys.Invoice.KeyPrice,
                                      Company = sdu.Company,
                                      FirstName = sdu.User.FirstName,
                                      SurName = sdu.User.SurName
                                  }
                             ).ToListAsync();

            foreach (var item in keysList)
            {
                int value;
                if (int.TryParse(item.Country, out value))
                {
                    var country = await _context.Countries.SingleOrDefaultAsync(a => a.Id == Convert.ToInt32(item.Country));
                    item.Country = country.Name;
                }

                if (int.TryParse(item.State_Province, out value))
                {
                    var state = await _context.States.SingleOrDefaultAsync(a => a.Id == Convert.ToInt32(item.State_Province));
                    item.State_Province = state.Name;
                }
                var keys = await _context.Activation.Where(a => a.ActivationKey == item.Code && a.IsDeleted == false).ToListAsync();
                foreach (var item1 in keys)
                {
                    var sdu = await _context.Sduactivation.Where(a => a.ActivationId == item1.ActivationId && a.IsDeleted == false).ToListAsync();
                    foreach (var item2 in sdu)
                    {
                        var leads = await _context.Leads.Where(a => a.Sduid == item2.SduactivationId).ToListAsync();
                        //foreach (var item3 in leads)
                        //{
                            if (leads != null && leads.Count != 0)
                            {
                                item.Leads = item.Leads + leads.Count();
                            }
                       // }
                    }
                }
            }

            return keysList;
        }
        public async Task<List<FinancialReconciliationReportViewModel>> FinancialReconciliationReportAsync(DateTime year, CancellationToken ct = default(CancellationToken))
        {
            var keysList = await (from keys in _context.Activation.Include(d => d.Invoice)
                                  join
                                  sdu in _context.Sduactivation.Include(a => a.User) on keys.ActivationId equals sdu.ActivationId
                                  join
                                  sh in _context.Show on sdu.ShowId equals sh.ShowId
                                  where sdu.CreatedDate.Value.Year == year.Year
                                  where keys.ActivationTypeId == 7 &&
                                  keys.IsDeleted == false &&
                                  sdu.IsDeleted == false &&
                                  sh.IsDeleted == false
                                  orderby sdu.CreatedDate descending
                                  select new FinancialReconciliationReportViewModel
                                  {
                                      InvoiceId = keys.InvoiceId,
                                      UnlockCode = keys.ActivationKey,
                                      FirstName = sdu.User.FirstName,
                                      SurName = sdu.User.SurName,
                                      Company = sdu.User.Company,
                                      Purchased = sdu.CreatedDate,
                                      Used = sdu.IsConsumed,
                                      ShowName = sh.ShowName,
                                      Unlocked = sdu.ActivationTime,
                                      KeyPrice = keys.Invoice.KeyPrice,

                                  }
                              ).OrderByDescending(a=>a.Purchased).ToListAsync();
            return keysList;
        }
        public async Task<AllResponse<PaymentReportVM>> GetPaymentReport(string year)
        {
            PaymentReportVM paymentReportVM = null;
            var result = new AllResponse<PaymentReportVM>
            {
                Data = new List<PaymentReportVM>()
            };
            var parameters = new List<DbParameter>
                {
                    base.GetParameter(YearParameterName, year)
                };
            using (var dataReader = await base.ExecuteReader(parameters, ReportRepository.GetStoredProcedureName, CommandType.StoredProcedure))
            {
                if (dataReader != null && dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        paymentReportVM = new PaymentReportVM
                        {

                            ID = dataReader.GetIntegerValue(ReportRepository.IDColumnName),
                            Year = dataReader.GetIntegerValue(ReportRepository.YearColumnName),
                            Reg_Ref = dataReader.GetStringValue(ReportRepository.Reg_RefColumnName),
                            FirstName = dataReader.GetStringValue(ReportRepository.FirstNameColumnName),
                            LastName = dataReader.GetStringValue(ReportRepository.LastNameColumnName),
                            AgencyID = dataReader.GetUnsignedIntegerValueNullable(ReportRepository.AgencyIDColumnName),
                            Campus = dataReader.GetUnsignedIntegerValueNullable(ReportRepository.CampusColumnName),
                            Format = dataReader.GetUnsignedIntegerValueNullable(ReportRepository.FormatColumnName),
                            AgentName = dataReader.GetStringValue(ReportRepository.AgentNameColumnName),
                            FormatName = dataReader.GetStringValue(ReportRepository.FormatNameColumnName),
                            CampusName = dataReader.GetStringValue(ReportRepository.CampusNameColumnName),
                            ProgramName = dataReader.GetStringValue(ReportRepository.ProgramNameColumnName),
                            TotalGrossPrice = dataReader.GetDoubleValue(ReportRepository.TotalGrossPriceColumnName),
                            TotalGrossPriceCalculated = dataReader.GetDoubleValue(ReportRepository.TotalGrossPriceCalculatedColumnName),
                            TotalAddins = dataReader.GetDoubleValue(ReportRepository.TotalAddinsColumnName),
                            Paid = dataReader.GetDoubleValue(ReportRepository.PaidColumnName),
                            TotalPaidPriceCalculated = dataReader.GetDoubleValue(ReportRepository.TotalPaidPriceCalculatedColumnName),
                            Commision = dataReader.GetDoubleValue(ReportRepository.CommisionColumnName),
                            CommissionAddins = dataReader.GetDoubleValue(ReportRepository.CommissionAddinsColumnName),
                            NetPrice = dataReader.GetDoubleValue(ReportRepository.NetPriceColumnName),
                            TotalNetPriceCalculated = dataReader.GetDoubleValue(ReportRepository.TotalNetPriceCalculatedColumnName),
                            Balance = dataReader.GetDoubleValue(ReportRepository.BalanceColumnName),
                            TotalBalanceCalculated = dataReader.GetDoubleValue(ReportRepository.TotalBalanceCalculatedColumnName),
                        };
                        result.Data.Add(paymentReportVM);
                    }

                    if (!dataReader.IsClosed)
                    {
                        dataReader.Close();
                    }
                }
                
            }

            return result;
        }
    }
}

