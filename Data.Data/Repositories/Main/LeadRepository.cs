using AutoMapper;
using ELI.Data.Context;
using ELI.Domain.Contracts.Main;
using ELI.Domain.Helpers;
using ELI.Domain.ViewModels;
using ELI.Entity.Main;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ELI.Data.Repositories.Main
{
    public class LeadRepository : ILeadsRepository
    {
        private readonly ELIContext _context;
        private readonly IMapper _mapper;
        string ConString;
        string ImgPath;
        public LeadRepository(ELIContext context, IMapper mapper)
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
        public async Task CreateLeadsAsync(SaveLeadViewModel leads, CancellationToken ct = default(CancellationToken))
        {

            var sdu = (from d in _context.Device
                       join sd in _context.Sduactivation
                       on d.DeviceId equals sd.DeviceId
                       join l in _context.Leads
                       on sd.SduactivationId equals l.Sduid
                       where d.IsActive == true && d.IsDeleted == false && sd.IsActive == true && sd.IsDeleted == false 
                       && (l.IsActive ?? false) == true && (l.IsDeleted ?? false) == false && l.Barcode == leads.Barcode
                       && d.DeviceIdentifier == leads.DeviceIdentifier && sd.ShowId == Convert.ToInt32(leads.ShowId)
                       select new {
                           LeadsId = l.LeadsId,
                           SduactivationId = l.Sduid
                       }).FirstOrDefault();
            if(sdu == null)
            {
                var show = _context.Show.FirstOrDefault(x => x.ShowId.ToString() == leads.ShowId);
                var device = _context.Device.FirstOrDefault(a => a.DeviceIdentifier == leads.DeviceIdentifier);
                var sduActivation = _context.Sduactivation.FirstOrDefault(a => a.ShowId == show.ShowId && a.IsActive == true && a.DeviceId == device.DeviceId);
                Leads newLead = new Leads();
                newLead.Barcode = leads.Barcode;
                newLead.IsDeleted = false;
                newLead.Sduid = sduActivation.SduactivationId;
                newLead.CreatedBy = sduActivation.UserId.ToString();
                _context.Leads.Add(newLead);
                await _context.SaveChangesAsync();

                sdu = (from d in _context.Device
                       join sd in _context.Sduactivation
                       on d.DeviceId equals sd.DeviceId
                       join l in _context.Leads
                       on sd.SduactivationId equals l.Sduid
                       where d.IsActive == true && d.IsDeleted == false && sd.IsActive == true && sd.IsDeleted == false
                       && (l.IsActive ?? false) == true && (l.IsDeleted ?? false) == false && l.Barcode == leads.Barcode
                       && d.DeviceIdentifier == leads.DeviceIdentifier && sd.ShowId == Convert.ToInt32(leads.ShowId)
                       select new
                       {
                           LeadsId = l.LeadsId,
                           SduactivationId = l.Sduid
                       }).FirstOrDefault();
            }
            if (sdu != null)
            {
                _context.LeadsQualifier.RemoveRange(_context.LeadsQualifier.Where(x => x.Sduid == sdu.SduactivationId && x.LeadsId == sdu.LeadsId));
                foreach (var item in leads.QualifierDetails)
                {
                    foreach (var answer in item.Response)
                    {
                        LeadsQualifier lead = new LeadsQualifier();
                        lead.LeadsId = sdu.LeadsId;
                        lead.Sduid = sdu.SduactivationId;
                        lead.QualifierId = Convert.ToInt32(leads.QualifierId);
                        lead.QuestionId = Convert.ToInt32(item.QuestionId);
                        lead.Response = answer;
                        lead.CreatedDate = DateTime.Now;
                        lead.IsActive = true;
                        lead.IsDeleted = false;
                        await _context.LeadsQualifier.AddAsync(lead, ct);
                    }
                    await _context.SaveChangesAsync(ct);
                }
                //}
                //else//update
                //{
                //    throw new AppException("Lead does not exist in the database.");
                //}
            }
            else
            {
                throw new AppException("Lead does not exist in the database.");
            }
        }
        public async Task<List<GetAllLeadsFromDevice>> GetLeadsByShowIdDeviceIdAsync(int showId, string DeviceIdentifier, CancellationToken ct = default(CancellationToken))
        {
            var leads1 = await (from d in _context.Device
                               join sd in _context.Sduactivation
                               on d.DeviceId equals sd.DeviceId
                               join l in _context.Leads
                               on sd.SduactivationId equals l.Sduid
                               where d.IsActive == true && d.IsDeleted == false && sd.IsActive == true && sd.IsDeleted == false
                               && (l.IsActive ?? false) == true && (l.IsDeleted ?? false) == false && l.IsActive == true && l.IsDeleted == false
                                && d.DeviceIdentifier == DeviceIdentifier && sd.ShowId == Convert.ToInt32(showId)
                               
                               select new GetAllLeadsFromDevice
                               {
                                   LeadsId = l.LeadsId,
                                   ShowId = Convert.ToString(showId),
                                   QualifierId = null,
                                   DeviceIdentifier = d.DeviceIdentifier,
                                   FullName =   (l.FirstName + " " + l.SurName),
                                   //Response = lq.Response,
                                   CreatedDate = l.CreatedDate,
                                   //QuestionId = lq.QuestionId,
                                   Barcode = l.Barcode,
                                   Sduid = l.Sduid
                               }
                     //select lq
                     ).Distinct().ToListAsync(); 



            var leads = await (from d in _context.Device
                               join sd in _context.Sduactivation
                               on d.DeviceId equals sd.DeviceId
                               join l in _context.Leads
                               on sd.SduactivationId equals l.Sduid
                               join lq in _context.LeadsQualifier
                               on l.LeadsId equals lq.LeadsId
                               where d.IsActive == true && d.IsDeleted == false && sd.IsActive == true && sd.IsDeleted == false
                               && (l.IsActive ?? false) == true && (l.IsDeleted ?? false) == false && l.IsActive == true && l.IsDeleted == false
                               && lq.Sduid == l.Sduid && d.DeviceIdentifier == DeviceIdentifier && sd.ShowId == Convert.ToInt32(showId)
                               orderby lq.CreatedDate
                               select new GetAllLeadsFromDevice
                               {
                                   LeadsId = l.LeadsId,
                                   ShowId = Convert.ToString(showId),
                                   QualifierId = lq.QualifierId,
                                   DeviceIdentifier = d.DeviceIdentifier,
                                   FullName = (l.FirstName + " " + l.SurName),
                                   //Response = lq.Response,
                                   CreatedDate = l.CreatedDate,
                                   //QuestionId = lq.QuestionId,
                                   Barcode = l.Barcode,
                                   Sduid = lq.Sduid
                               }
                       //select lq
                       ).Distinct().ToListAsync();

            foreach (var item in leads1)
            {
                if(!(leads.Any(x => x.LeadsId == item.LeadsId)))
                {
                    leads.Add(item);
                }
            }
            foreach (var lead in leads)
            {
                if(lead.QualifierId != null && lead.QualifierId > 0 )
                {
                    lead.Response = GetLeadsByLeadIdQId(lead.LeadsId, (int)lead.QualifierId, ct).Result;
                }
            }

            if (leads != null)
            {
                return leads;
            }
            else
            {
                throw new AppException("No Device associated with this Device Identifier.");
            }

            
        }

        public async Task<List<LeadsQualifier>> GetLeadsByLeadIdQId(int LeadId, int QualifierId, CancellationToken ct = default(CancellationToken))
        {
            var leads = await (from  lq in _context.LeadsQualifier
                               where lq.LeadsId == LeadId && lq.QualifierId == QualifierId && lq.IsDeleted == false
                               select lq
                       ).Distinct().ToListAsync();

            if (leads != null)
            {
                return leads;
            }
            else
            {
                throw new AppException("No Data Found.");
            }
            
        }

        
    }
}
