using ELI.Data.Context;
using ELI.Domain.Contracts.Main;
using ELI.Domain.Helpers;
using ELI.Domain.ViewModels;
using ELI.Entity.Main;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ELI.Data.Repositories.Main
{
    public class ReportRepository : IReportRepository
    {
        private readonly ELIContext _context;
        public ReportRepository(ELIContext context)
        {
            _context = context;
        }
        public void Dispose()
        {
            _context.Dispose();
        }
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
    }
}

