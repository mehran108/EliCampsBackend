using AutoMapper;
using ELI.Data.Context;
using ELI.Domain.Contracts.Main;
using ELI.Domain.Helpers;
using ELI.Domain.ViewModels;
using ELI.Entity.Auth;
using ELI.Entity.Main;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace ELI.Data.Repositories.Main
{
    public class QualifierRepository : IQualifierRepository
    {
        private readonly ELIContext _context;
        private readonly ELIAuthDbContext _userContext;
        private readonly IMapper _mapper;
        private readonly IQuestionRepository _questionRepository;
        string UserName;
        string Password;
        string GetLeadsInfoLink;
        string ConString;
        string ImgPath;
        public QualifierRepository(ELIContext context, ELIAuthDbContext userContext, IMapper mapper, IQuestionRepository questionRepository)
        {
            _userContext = userContext;
            _context = context;
            _mapper = mapper;
            var builder = new ConfigurationBuilder()
          .AddJsonFile("appsettings.json")
          .AddJsonFile($"appsettings.json", true)
          .AddEnvironmentVariables();
            var config = builder.Build();
            _questionRepository = questionRepository;
            ConString = config.GetConnectionString("ELIDb");
            ImgPath = config.GetSection("ConnectionStrings").GetSection("ImagePathKey").Value;
            UserName = config.GetSection("ConnectionStrings").GetSection("UserName").Value;
            Password = config.GetSection("ConnectionStrings").GetSection("Password").Value;
            GetLeadsInfoLink = config.GetSection("ConnectionStrings").GetSection("GetLeadsInfoLink").Value;
        }
        public void Dispose()
        {
            _context.Dispose();
        }
        public async Task<List<Qualifier>> GetAllQualifiersAsync(CancellationToken ct = default(CancellationToken))
        {
            /*all admin qualifiers either published or not, should not be deleted or inactive*/
            return await (from qual in _context.Qualifier
                          join sh in _context.Show
                          on qual.ShowId equals sh.ShowId
                          join us in _userContext.Users
                          on qual.OwnerId equals Convert.ToString(us.Id)
                          join ut in _userContext.UserRoles
                          on us.Id equals ut.UserId
                          where ut.RoleId == 1 && qual.IsDeleted == false && qual.IsActive == true //&& qual.IsPublished == true
                          select qual).Include(x => x.show)
                                  .Union(_context.Qualifier.Where(l => l.ShowId == null && l.IsDefault == true && l.IsDeleted == false && l.IsActive == true)) //&& l.IsPublished == true))
                                  .OrderByDescending(x => x.CreatedDate)
                                  .ToListAsync();
            //var t = await (from s in _context.Qualifier.Include(a => a.show)
            //               join sh in _context.Show on s.ShowId equals sh.ShowId
            //               where
            //             s.IsActive == true &&
            //              s.IsDeleted == false &&
            //              s.IsAdmin == true &&
            //            //sh.IsActive == true &&
            //            sh.IsDeleted == false
            //               select s
            //                    ).Union(_context.Qualifier.Where(l => l.ShowId.HasValue == false))
            //                    .OrderByDescending(x => x.CreatedDate)
            //                   .ToListAsync();
            ////orderby s.CreatedDate
            //return t;
        }
        public async Task<List<Qualifier>> GetAllQualifiersExhibitorsAsync(int userid, CancellationToken ct = default(CancellationToken))
        {
            /*all exhibitor qualifiers either published or not, should not be deleted ot inactive
             only published admin qualifiers will be visible here.
             */
            var adminQualifers = await GetAllQualifiersAsync(ct);

            return await (from qual in _context.Qualifier
                          where qual.OwnerId == Convert.ToString(userid) && qual.IsActive == true && qual.IsDeleted == false
                          select qual)
                                   .Union(adminQualifers.Where(x => x.IsPublished == true)).ToListAsync();
            //List<Qualifier> qa = new List<Qualifier>();
            //var t = await (from s in _context.Qualifier.Include(a => a.show)
            //               join sh in _context.Show on s.ShowId equals sh.ShowId
            //               join sdu in _context.Sduactivation on s.ShowId equals sdu.ShowId
            //               where
            //              s.IsActive == true &&
            //              s.IsDeleted == false &&
            //              (s.CreatedBy == null || s.CreatedBy == userid+"") &&
            //            sh.IsDeleted == false && sdu.UserId == userid
            //            && sdu.IsDeleted == false && ((s.IsPublished == true && s.IsAdmin == true) || s.IsAdmin == false)
            //               orderby s.CreatedDate
            //               select s
            //                    ).ToListAsync();
            //if (t != null && t.Count != 0)
            //{
            //    qa = t.Distinct().ToList();
            //}
            //return qa;
        }
        public async Task<Qualifier> CreateQualifierAsync(Qualifier qualifier, string DeviceIdentifier, CancellationToken ct = default(CancellationToken))
        {
            var device = await _context.Device.SingleOrDefaultAsync(a => a.DeviceIdentifier == DeviceIdentifier && a.IsActive == true && a.IsDeleted == false);
            if (device != null)
            {
                qualifier.DeviceId = device.DeviceId;
                await _context.Qualifier.AddAsync(qualifier, ct);
            }
            await _context.SaveChangesAsync(ct);
            return qualifier;
        }
        public async Task<List<Qualifier>> GetQualifierByShowIdAsync(int showId, string DeviceIdentifier, CancellationToken ct = default(CancellationToken))
        {
            List<Qualifier> QL = new List<Qualifier>();

            var device = await _context.Device.SingleOrDefaultAsync(a => a.DeviceIdentifier == DeviceIdentifier && a.IsDeleted == false && a.IsActive == true);
            if (device != null)
            {
                var sdu = await _context.Sduactivation.SingleOrDefaultAsync(x => x.ShowId == showId && x.DeviceId == device.DeviceId && x.IsActive == true && x.IsDeleted == false);
                /*get list of all admin & exhibitor qualifiers*/
                var adminQualifiers = (await GetAllQualifiersAsync()).Where(x => x.IsPublished == true && (x.ShowId == null || x.ShowId == showId)).ToList();
                foreach (var qual in adminQualifiers)
                {
                    var questions = (await GetQuestionsByQualifierIdAsync("Admin", qual.QualifierId, Convert.ToString(sdu.UserId), 0, ct)).Questions;
                    if (questions != null)
                    {
                        qual.Questions = questions.Where(x => (x.ShowId == null ? true : (int)x.ShowId == showId)).ToList();
                    }
                }
                var exhibitorQualifiers = (await GetAllQualifiersExhibitorsAsync(sdu.UserId)).Where(x => x.ShowId == showId && x.IsPublished == true);
                foreach (var qual in exhibitorQualifiers)
                {
                    var questions = (await GetQuestionsByQualifierIdAsync("Exhibitor", qual.QualifierId, Convert.ToString(sdu.UserId), 0, ct)).Questions;
                    if (questions != null)
                    {
                        qual.Questions = questions.Where(x => (x.ShowId == null ? true : (int)x.ShowId == showId)).ToList();
                    }
                }
                /*get list of all device qualifiers*/
                var deviceQualifiers = await (from qual in _context.Qualifier
                                              where qual.ShowId == sdu.ShowId && qual.DeviceId == sdu.DeviceId && qual.IsActive == true && qual.IsDeleted == false
                                              select qual)
                                       .Union(adminQualifiers)
                                       .Union(exhibitorQualifiers)
                                       .OrderByDescending(x => x.IsDefault)
                                       .ThenBy(x => x.CreatedDate)
                                       .ToListAsync();
                foreach (var qual in deviceQualifiers)
                {
                    qual.Questions = (await GetQuestionsByQualifierIdAsync("", qual.QualifierId, Convert.ToString(sdu.UserId), (sdu.DeviceId ?? 0), ct)).Questions;
                }
                return deviceQualifiers;

            }
            else
                throw new AppException("No Device associated with it.");
        }
        public async Task<List<Qualifier>> GetQualifierByQIdAsync(int showId, int QId, string DeviceIdentifier, CancellationToken ct = default(CancellationToken))
        {
            List<Qualifier> QL = new List<Qualifier>();

            var device = await _context.Device.SingleOrDefaultAsync(a => a.DeviceIdentifier == DeviceIdentifier && a.IsDeleted == false && a.IsActive == true);
            if (device != null)
            {

                /*get list of all device qualifiers*/
                var deviceQualifiers = await (from qual in _context.Qualifier
                                              where qual.QualifierId == QId && qual.IsActive == true && qual.IsDeleted == false
                                              select qual)
                                       .OrderByDescending(x => x.IsDefault)
                                       .ThenBy(x => x.CreatedDate)
                                       .ToListAsync();

                var sdu = await _context.Sduactivation.SingleOrDefaultAsync(x => x.ShowId == showId && x.DeviceId == device.DeviceId && x.IsActive == true && x.IsDeleted == false);

                foreach (var qual in deviceQualifiers)
                {
                    qual.Questions = (await GetQuestionsByQualifierIdAsync("", qual.QualifierId, Convert.ToString(sdu.UserId), (device.DeviceId), ct)).Questions;
                }
                return deviceQualifiers;

            }
            else
                throw new AppException("No Device associated with it.");
        }
        public async Task<List<CreateQuestionViewModel>> GetQuestionsByQualifierIdFromDevice(int qualifierId, int showId, string deviceIdentifier, CancellationToken ct = default(CancellationToken))
        {
            List<CreateQuestionViewModel> questionList = new List<CreateQuestionViewModel>();
            var device = await _context.Device.SingleOrDefaultAsync(a => a.DeviceIdentifier == deviceIdentifier && a.IsDeleted == false && a.IsActive == true);
            if (device != null)
            {
                var sdu = await _context.Sduactivation.SingleOrDefaultAsync(x => x.ShowId == showId && x.DeviceId == device.DeviceId && x.IsActive == true && x.IsDeleted == false);
                var deviceQuestions = (await GetQuestionsByQualifierIdAsync("", qualifierId, Convert.ToString(sdu.UserId), device.DeviceId, ct)).Questions;
                foreach (var ques in deviceQuestions)
                {
                    if (ques.ShowId != null && ques.ShowId != showId) continue;

                    CreateQuestionViewModel questionVM = new CreateQuestionViewModel();
                    //var questions = _mapper.Map<CreateQuestionViewModel>(ques);
                    questionVM.QualifierId = ques.QualifierId;
                    questionVM.QuestionDescription = ques.QuestionDescription;
                    questionVM.QuestionId = ques.QuestionId;
                    questionVM.QuestionTypeId = ques.QuestionTypeId;
                    questionVM.QuestionTypeName = ques.QuestionTypeName;
                    questionVM.Sequence = ques.Sequence;
                    var deviceQuestion = await _context.Device.SingleOrDefaultAsync(a => a.DeviceId == ques.DeviceId);
                    if (deviceQuestion != null)
                    {
                        questionVM.DeviceIdentifier = deviceQuestion.DeviceIdentifier;
                    }
                    questionVM.Responses = await _context.QuestionOption.OrderBy(a => a.Sequence).Where(a => a.QuestionId == ques.QuestionId && a.IsActive == true && a.IsDeleted == false).ToListAsync();
                    questionList.Add(questionVM);
                }
            }
            else
                throw new AppException("No Device associated with it.");
            return questionList.OrderBy(x => x.Sequence).ToList();
        }
        public async Task<Qualifier> GetQuestionsByQualifierIdAsync(string role, int Id, string userId, int deviceId = 0, CancellationToken ct = default(CancellationToken))
        {
            var qualifier = await _context.Qualifier.SingleOrDefaultAsync(a => a.QualifierId == Id && a.IsActive == true && a.IsDeleted == false);
            if (qualifier != null)
            {
                var adminQuestions = await (from ques in _context.Question
                                            join qual in _context.Qualifier
                                            on ques.QualifierId equals qual.QualifierId
                                            join u in _userContext.Users
                                            on ques.OwnerId equals Convert.ToString(u.Id)
                                            join ur in _userContext.UserRoles
                                            on u.Id equals ur.UserId
                                            where qual.QualifierId == qualifier.QualifierId && ur.RoleId == 1 && ques.IsActive == true && ques.IsDeleted == false && ques.IsAdmin == true
                                            orderby ques.Sequence
                                            select ques).ToListAsync();

                if (role == "Admin")
                {
                    qualifier.Questions = adminQuestions;
                }
                else //Exhibitor
                {
                    var exhibitorQuestions = await (from ques in _context.Question
                                                    join qual in _context.Qualifier
                                                    on ques.QualifierId equals qual.QualifierId
                                                    join u in _userContext.Users
                                                    on ques.OwnerId equals Convert.ToString(u.Id)
                                                    join ut in _userContext.UserRoles
                                                    on u.Id equals ut.UserId
                                                    where qual.QualifierId == qualifier.QualifierId && ut.RoleId == 2 && ques.OwnerId == userId && ques.IsActive == true && ques.IsDeleted == false
                                                    select ques)
                                                .Union(adminQuestions)
                                                 .OrderBy(l => l.Sequence)
                                                 .ToListAsync();

                    if (deviceId != 0)//device questions 
                    {
                        var deviceQuestions = await (from ques in _context.Question
                                                     join qual in _context.Qualifier
                                                     on ques.QualifierId equals qual.QualifierId
                                                     where qual.QualifierId == qualifier.QualifierId && ques.DeviceId == deviceId && ques.IsActive == true && ques.IsDeleted == false
                                                     select ques)
                                                .Union(adminQuestions)
                                                .Union(exhibitorQuestions)
                                                 .OrderBy(l => l.Sequence)
                                                 .ToListAsync();
                        qualifier.Questions = deviceQuestions;
                    }
                    else
                    {
                        qualifier.Questions = exhibitorQuestions;
                    }
                }
                foreach (var d in qualifier.Questions)
                {
                    d.Responses = await _context.QuestionOption.Where(a => a.QuestionId == d.QuestionId && a.IsActive == true && a.IsDeleted == false).ToListAsync();
                }
            }
            return qualifier;
        }
        public async Task<GetLeadsInfoViewModel> GetLeadsInfoAsync(int Id, string ShowKey, string deviceIdentifier, string barcode, CancellationToken ct = default(CancellationToken))
        {
            var existingLead = (from s in _context.Show
                                join sd in _context.Sduactivation
                                on s.ShowId equals sd.ShowId
                                join l in _context.Leads
                                on sd.SduactivationId equals l.Sduid
                                join d in _context.Device
                                on sd.DeviceId equals d.DeviceId
                                where s.ShowKey == ShowKey && l.Barcode == barcode && d.DeviceIdentifier == deviceIdentifier && sd.IsActive == true
                                select l).FirstOrDefault();
            if (existingLead != null)
            {
                return _mapper.Map<GetLeadsInfoViewModel>(existingLead);
            }
            var sdu = new Sduactivation();
            //var sduId = 1;
            //var sduuserid = 1;

            var show = await _context.Show.SingleOrDefaultAsync(a => a.ShowKey == ShowKey);
            if (show != null)
            {
                var device = await _context.Device.FirstOrDefaultAsync(a => a.DeviceIdentifier == deviceIdentifier);
                if (device != null)
                {
                    sdu = await _context.Sduactivation.FirstOrDefaultAsync(a => a.ShowId == show.ShowId && a.IsActive == true && a.DeviceId == device.DeviceId);
                    //if (sdu != null)
                    //{
                    //    sduId = sdu.SduactivationId;
                    //    sduuserid = sdu.UserId;
                    //}
                }
                if (show.DatabaseId != null)
                {
                    var database = await _context.Database.SingleOrDefaultAsync(a => a.DatabaseId == show.DatabaseId);
                    if (database != null)
                    {
                        var Leadslink = database.LeadsInfoURL + "?op=getLeadsInfo";
                        var client = new RestClient(Leadslink);
                        var request = new RestRequest(Method.POST);
                        request.AddHeader("Postman-Token", "1bf33fc8-f1ce-46b2-9949-54be05c1ac9e");
                        request.AddHeader("Cache-Control", "no-cache");
                        request.AddHeader("Content-Type", "text/xml");
                        request.AddParameter("undefined", "<soap:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\">\r\n  <soap:Header>\r\n    <AuthHeader xmlns=\"http://tempuri.org/\">\r\n      <Username>" + database.UserName + "</Username>\r\n      <Password>" + database.Password + "</Password>\r\n    </AuthHeader>\r\n  </soap:Header>\r\n  <soap:Body>\r\n    <getLeadsInfo xmlns=\"http://tempuri.org/\">\r\n      <param1></param1>\r\n      <param2>" + show.DbName + "</param2>\r\n      <barcodeList>'" + barcode + "'</barcodeList>\r\n    </getLeadsInfo>\r\n  </soap:Body>\r\n</soap:Envelope>", ParameterType.RequestBody);
                        IRestResponse response = client.Execute(request);
                        if (response.IsSuccessful == true)
                        {
                            var xdoc = XDocument.Parse(response.Content);

                            var list = ((System.Xml.Linq.XElement)xdoc.FirstNode).Value.TrimStart('[').TrimStart('{').TrimEnd(']').TrimEnd('}').Split(',');

                            GetLeadsInfoViewModel V = new GetLeadsInfoViewModel();

                            foreach (var item in list)
                            {
                                var obj = item.Split(':');

                                if (obj[0].Trim('"') == "c")
                                {
                                    V.FirstName = obj[1].Trim('"');
                                }
                                else if (obj[0].Trim('"') == "d")
                                {
                                    V.LastName = obj[1].Trim('"');
                                }
                                else if (obj[0].Trim('"') == "e")
                                {
                                    V.Company = obj[1].Trim('"');
                                }
                                else if (obj[0].Trim('"') == "f")
                                {
                                    V.Designation = obj[1].Trim('"');
                                }
                                else if (obj[0].Trim('"') == "g")
                                {
                                    V.Address = obj[1].Trim('"');
                                }
                                else if (obj[0].Trim('"') == "l")
                                {
                                    V.Country = obj[1].Trim('"');
                                }
                                else if (obj[0].Trim('"') == "k")
                                {
                                    V.CountryCode = obj[1].Trim('"');
                                }
                                else if (obj[0].Trim('"') == "t")
                                {
                                    V.Phone = obj[1].Trim('"');
                                }
                                else if (obj[0].Trim('"') == "u")
                                {
                                    V.Email = obj[1].Trim('"');
                                }
                            }
                            var leadExisted = await _context.Leads.SingleOrDefaultAsync(a => a.Barcode == barcode && a.Sduid == sdu.SduactivationId);
                            if (leadExisted == null)
                            {
                                Leads Lead = new Leads();
                                Lead.Address = V.Address;
                                Lead.Barcode = barcode;
                                Lead.Company = V.Company;
                                Lead.Country = V.Country;
                                Lead.CountryCode = V.CountryCode;
                                Lead.FirstName = V.FirstName;
                                Lead.SurName = V.LastName;
                                Lead.Designation = V.Designation;
                                Lead.Phone = V.Phone;
                                Lead.Email = V.Email;
                                Lead.Sduid = sdu.SduactivationId;
                                Lead.CreatedBy = sdu.UserId.ToString();
                                Lead.IsDeleted = false;
                                _context.Leads.Add(Lead);
                                await _context.SaveChangesAsync();
                            }
                            else
                            {
                                leadExisted.Address = V.Address;
                                leadExisted.Barcode = barcode;
                                leadExisted.Company = V.Company;
                                leadExisted.Country = V.Country;
                                leadExisted.CountryCode = V.CountryCode;
                                leadExisted.FirstName = V.FirstName;
                                leadExisted.SurName = V.LastName;
                                leadExisted.Designation = V.Designation;
                                leadExisted.Phone = V.Phone;
                                leadExisted.Email = V.Email;
                                leadExisted.Sduid = sdu.SduactivationId;
                                leadExisted.CreatedBy = sdu.UserId.ToString();
                                _context.Leads.Update(leadExisted);
                                await _context.SaveChangesAsync();
                            }

                            return V;
                        }
                        else
                        {
                            throw new AppException("URL link is not correct");
                        }
                    }
                    else
                    {
                        throw new AppException("No Database associated with this show");
                    }
                }
                else
                {
                    throw new AppException("No Database associated with this show");
                }
            }
            else
            {
                throw new AppException("No Show associated with this show key");
            }
        }
        private GetLeadsInfoViewModel GetMergedLead(GetLeadsInfoViewModel externalLead, GetLeadsInfoViewModel mobileLead)
        {
            var updatedLead = new GetLeadsInfoViewModel();
            if (string.IsNullOrEmpty(mobileLead.Address) && !string.IsNullOrEmpty(externalLead.Address))
                updatedLead.Address = externalLead.Address;
            else
                updatedLead.Address = mobileLead.Address;
            if (string.IsNullOrEmpty(mobileLead.Company) && !string.IsNullOrEmpty(externalLead.Company))
                updatedLead.Company = externalLead.Company;
            else
                updatedLead.Company = mobileLead.Company;
            if (string.IsNullOrEmpty(mobileLead.Country) && !string.IsNullOrEmpty(externalLead.Country))
                updatedLead.Country = externalLead.Country;
            else
                updatedLead.Country = mobileLead.Country;
            if (string.IsNullOrEmpty(mobileLead.CountryCode) && !string.IsNullOrEmpty(externalLead.CountryCode))
                updatedLead.CountryCode = externalLead.CountryCode;
            else
                updatedLead.CountryCode = mobileLead.CountryCode;
            if (string.IsNullOrEmpty(mobileLead.Designation) && !string.IsNullOrEmpty(externalLead.Designation))
                updatedLead.Designation = externalLead.Designation;
            else
                updatedLead.Designation = mobileLead.Designation;
            if (string.IsNullOrEmpty(mobileLead.Email) && !string.IsNullOrEmpty(externalLead.Email))
                updatedLead.Email = externalLead.Email;
            else
                updatedLead.Email = mobileLead.Email;
            if (string.IsNullOrEmpty(mobileLead.FirstName) && !string.IsNullOrEmpty(externalLead.FirstName))
                updatedLead.FirstName = externalLead.FirstName;
            else
                updatedLead.FirstName = mobileLead.FirstName;
            if (string.IsNullOrEmpty(mobileLead.LastName) && !string.IsNullOrEmpty(externalLead.LastName))
                updatedLead.LastName = externalLead.LastName;
            else
                updatedLead.LastName = mobileLead.LastName;
            if (string.IsNullOrEmpty(mobileLead.Phone) && !string.IsNullOrEmpty(externalLead.Phone))
                updatedLead.Phone = externalLead.Phone;
            else
                updatedLead.Phone = mobileLead.Phone;
            if (string.IsNullOrEmpty(mobileLead.Address2) && !string.IsNullOrEmpty(externalLead.Address2))
                updatedLead.Address2 = externalLead.Address2;
            else
                updatedLead.Address2 = mobileLead.Address2;
            if (string.IsNullOrEmpty(mobileLead.State) && !string.IsNullOrEmpty(externalLead.State))
                updatedLead.State = externalLead.State;
            else
                updatedLead.State = mobileLead.State;
            if (string.IsNullOrEmpty(mobileLead.Suburb) && !string.IsNullOrEmpty(externalLead.Suburb))
                updatedLead.Suburb = externalLead.Suburb;
            else
                updatedLead.Suburb = mobileLead.Suburb;
            if (string.IsNullOrEmpty(mobileLead.Landline) && !string.IsNullOrEmpty(externalLead.Landline))
                updatedLead.Landline = externalLead.Landline;
            else
                updatedLead.Landline = mobileLead.Landline;
            return updatedLead;
        }
        public async Task<GetLeadsInfoViewModel> UpdateLeadsInfoAsync(int Id, string ShowKey, string deviceIdentifier, string barcode, string scannedDatetime, GetLeadsInfoViewModel leadsVM, CancellationToken ct = default(CancellationToken))
        {
            GetLeadsInfoViewModel updateLead = default(GetLeadsInfoViewModel);
            var existingLead = (from s in _context.Show
                                join sd in _context.Sduactivation
                                on s.ShowId equals sd.ShowId
                                join l in _context.Leads
                                on sd.SduactivationId equals l.Sduid
                                join d in _context.Device
                                on sd.DeviceId equals d.DeviceId
                                where s.ShowKey == ShowKey && l.Barcode == barcode && d.DeviceIdentifier == deviceIdentifier && sd.IsActive == true
                                select l).FirstOrDefault();
            var show = _context.Show.FirstOrDefault(x => x.ShowKey == ShowKey);
            var device = _context.Device.FirstOrDefault(a => a.DeviceIdentifier == deviceIdentifier);
            var sduActivation = _context.Sduactivation.FirstOrDefault(a => a.ShowId == show.ShowId && a.IsActive == true && a.DeviceId == device.DeviceId);
            if (existingLead != null)
            {
                updateLead = GetMergedLead(_mapper.Map<GetLeadsInfoViewModel>(existingLead), leadsVM); //leadsVM;
                existingLead.Address = updateLead.Address;
                existingLead.Barcode = barcode;
                existingLead.Company = updateLead.Company;
                existingLead.Country = updateLead.Country;
                existingLead.CountryCode = updateLead.CountryCode;
                existingLead.FirstName = updateLead.FirstName;
                existingLead.SurName = updateLead.LastName;
                existingLead.Designation = updateLead.Designation;
                existingLead.Phone = updateLead.Phone;
                existingLead.Email = updateLead.Email;
                existingLead.IsDeleted = false;
                existingLead.Sduid = sduActivation.SduactivationId;
                existingLead.CreatedBy = sduActivation.UserId.ToString();
                existingLead.CreatedDate = Convert.ToDateTime(scannedDatetime);
                _context.Leads.Update(existingLead);
                await _context.SaveChangesAsync();
                return updateLead;
            }
            else
            {
                var externalLead = await GetLeadFromExternalAPI(show, ShowKey, barcode);
                updateLead = GetMergedLead(externalLead, leadsVM);
                Leads newLead = new Leads();
                newLead.Address = updateLead.Address;
                newLead.Barcode = barcode;
                newLead.Company = updateLead.Company;
                newLead.Country = updateLead.Country;
                newLead.CountryCode = updateLead.CountryCode;
                newLead.FirstName = updateLead.FirstName;
                newLead.SurName = updateLead.LastName;
                newLead.Designation = updateLead.Designation;
                newLead.Phone = updateLead.Phone;
                newLead.Email = updateLead.Email;
                newLead.Address2 = updateLead.Address2;
                newLead.State = updateLead.State;
                newLead.Suburb = updateLead.Suburb;
                newLead.Landline = updateLead.Landline;
                newLead.IsDeleted = false;
                newLead.Sduid = sduActivation.SduactivationId;
                newLead.CreatedBy = sduActivation.UserId.ToString();
                newLead.CreatedDate = Convert.ToDateTime(scannedDatetime);
                _context.Leads.Add(newLead);
                await _context.SaveChangesAsync();
                return updateLead;
            }
        }

        public async Task<List<Leads>> GetLeadsByShowIdAsync(int showId, CancellationToken ct = default(CancellationToken))
        {
            Show show = _context.Show.SingleOrDefault(a => a.ShowId == showId);
            var leads = await (from s in _context.Show
                               join sd in _context.Sduactivation
                                on s.ShowId equals sd.ShowId
                               join l in _context.Leads
                               on sd.SduactivationId equals l.Sduid
                               where s.IsDeleted == false &&
                               sd.IsActive == true && sd.IsDeleted == false
                                && (l.IsDeleted ?? false) == false
                                && l.IsDeleted == false
                                && sd.ShowId == Convert.ToInt32(showId)
                               && (l.FirstName == ""
                                || l.SurName == ""
                                || l.Company == ""
                                || l.Designation == ""
                                || l.Email == ""
                                || l.Phone == ""
                                || l.Country == ""
                                || l.Address2 == ""
                                || l.State == ""
                                || l.Suburb == ""
                                || l.Landline == ""
                                || l.FirstName == null
                                || l.SurName == null
                                || l.Company == null
                                || l.Designation == null
                                || l.Email == null
                                || l.Phone == null
                                || l.Country == null
                                || l.Address2 == null
                                || l.State == null
                                || l.Suburb == null
                                || l.Landline == null)

                               select new Leads
                               {
                                   LeadsId = l.LeadsId,
                                   FirstName = l.FirstName,
                                   SurName = l.SurName,
                                   Company = l.Company,
                                   Designation = l.Designation,
                                   Address = l.Address,
                                   Country = l.Country,
                                   CountryCode = l.CountryCode,
                                   Phone = l.Phone,
                                   Email = l.Email,
                                   Barcode = l.Barcode,
                                   Sduid = Convert.ToInt32(l.Sduid),
                                   CreatedBy = l.CreatedBy,
                                   CreatedDate = l.CreatedDate,
                                   IsDeleted = l.IsDeleted,
                                   IsActive = l.IsActive,
                                   Address2 = l.Address2,
                                   State = l.State,
                                   Suburb = l.Suburb,
                                   Landline = l.Landline
                               }

                     ).ToListAsync();

            if (leads != null)
            {

                if (show != null)
                {
                    if (show.DatabaseId != null)
                    {
                        var database = await _context.Database.SingleOrDefaultAsync(a => a.DatabaseId == show.DatabaseId);
                        if (database != null)
                        {
                            List<Leads> updatedLeadList = new List<Leads>();
                            string barcodeList = "";
                            foreach (var item in leads)
                            {
                                barcodeList = barcodeList + "'" + item.Barcode + "',";
                            }
                            barcodeList = barcodeList.TrimEnd(',');

                            var Leadslink = database.LeadsInfoURL + "?op=getLeadsInfo";
                            var client = new RestClient(Leadslink);
                            var request = new RestRequest(Method.POST);
                            request.AddHeader("Postman-Token", "1bf33fc8-f1ce-46b2-9949-54be05c1ac9e");
                            request.AddHeader("Cache-Control", "no-cache");
                            request.AddHeader("Content-Type", "text/xml");
                            request.AddParameter("undefined", "<soap:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\">\r\n  <soap:Header>\r\n    <AuthHeader xmlns=\"http://tempuri.org/\">\r\n      <Username>" + database.UserName + "</Username>\r\n      <Password>" + database.Password + "</Password>\r\n    </AuthHeader>\r\n  </soap:Header>\r\n  <soap:Body>\r\n    <getLeadsInfo xmlns=\"http://tempuri.org/\">\r\n      <param1></param1>\r\n      <param2>" + show.DbName + "</param2>\r\n      <barcodeList>" + barcodeList + "</barcodeList>\r\n    </getLeadsInfo>\r\n  </soap:Body>\r\n</soap:Envelope>", ParameterType.RequestBody);
                            IRestResponse response = client.Execute(request);
                            if (response.IsSuccessful == true)
                            {
                                var xdoc = XDocument.Parse(response.Content);
                                var responselist = ((System.Xml.Linq.XElement)xdoc.FirstNode).Value.TrimStart('[').Split("},{");


                                foreach (var item in responselist)
                                {

                                    var list = item.TrimStart('{').TrimEnd(']').TrimEnd('}').Split(',');


                                    GetLeadsInfoViewModel V = new GetLeadsInfoViewModel();

                                    V.Landline = "";
                                    foreach (var item1 in list)
                                    {
                                        var obj = item1.Split(':');

                                        if (obj[0].Trim('"') == "a")
                                        {
                                            V.BarCode = obj[1].Trim('"');
                                        }
                                        else if (obj[0].Trim('"') == "c")
                                        {
                                            V.FirstName = obj[1].Trim('"');
                                        }
                                        else if (obj[0].Trim('"') == "d")
                                        {
                                            V.LastName = obj[1].Trim('"');
                                        }
                                        else if (obj[0].Trim('"') == "e")
                                        {
                                            V.Company = obj[1].Trim('"');
                                        }
                                        else if (obj[0].Trim('"') == "f")
                                        {
                                            V.Designation = obj[1].Trim('"');
                                        }
                                        else if (obj[0].Trim('"') == "g")
                                        {
                                            V.Address = obj[1].Trim('"');
                                        }
                                        else if (obj[0].Trim('"') == "h")
                                        {
                                            V.Address2 = obj[1].Trim('"');
                                        }
                                        else if (obj[0].Trim('"') == "i")
                                        {
                                            V.Suburb = obj[1].Trim('"');
                                        }
                                        else if (obj[0].Trim('"') == "j")
                                        {
                                            V.State = obj[1].Trim('"');
                                        }
                                        else if (obj[0].Trim('"') == "l")
                                        {
                                            V.Country = obj[1].Trim('"');
                                        }
                                        else if (obj[0].Trim('"') == "m")
                                        {
                                            V.Landline = V.Landline + " " + obj[1].Trim('"');
                                        }
                                        else if (obj[0].Trim('"') == "n")
                                        {
                                            V.Landline = V.Landline + " " + obj[1].Trim('"');
                                        }
                                        else if (obj[0].Trim('"') == "o")
                                        {
                                            V.Landline = V.Landline + " " + obj[1].Trim('"');
                                        }
                                        else if (obj[0].Trim('"') == "k")
                                        {
                                            V.CountryCode = obj[1].Trim('"');
                                        }
                                        else if (obj[0].Trim('"') == "t")
                                        {
                                            V.Phone = obj[1].Trim('"');
                                        }
                                        else if (obj[0].Trim('"') == "u")
                                        {
                                            V.Email = obj[1].Trim('"');
                                        }
                                    }
                                    if (V.FirstName != null) V.FirstName = V.FirstName.Replace("null", "");
                                    if (V.LastName != null) V.LastName = V.LastName.Replace("null", "");
                                    if (V.Company != null) V.Company = V.Company.Replace("null", "");
                                    if (V.Designation != null) V.Designation = V.Designation.Replace("null", "");
                                    if (V.Email != null) V.Email = V.Email.Replace("null", "");
                                    if (V.Phone != null) V.Phone = V.Phone.Replace("null", "");
                                    if (V.Country != null) V.Country = V.Country.Replace("null", "");
                                    if (V.Address2 != null) V.Address2 = V.Address2.Replace("null", "");
                                    if (V.State != null) V.State = V.State.Replace("null", "");
                                    if (V.Suburb != null) V.Suburb = V.Suburb.Replace("null", "");
                                    if (V.Landline != null) V.Landline = V.Landline.Replace("null", "");

                                    foreach (var item1 in leads.Where(x => x.Barcode == V.BarCode))
                                    {
                                        if (string.IsNullOrEmpty(item1.FirstName))
                                        {
                                            item1.FirstName = V.FirstName;
                                        }
                                        if (string.IsNullOrEmpty(item1.SurName))
                                        {
                                            item1.SurName = V.LastName;
                                        }
                                        if (string.IsNullOrEmpty(item1.Company))
                                        {
                                            item1.Company = V.Company;
                                        }
                                        if (string.IsNullOrEmpty(item1.Designation))
                                        {
                                            item1.Designation = V.Designation;
                                        }
                                        if (string.IsNullOrEmpty(item1.Email))
                                        {
                                            item1.Email = V.Email;
                                        }
                                        if (string.IsNullOrEmpty(item1.Phone))
                                        {
                                            item1.Phone = V.Phone;
                                        }
                                        if (string.IsNullOrEmpty(item1.Country))
                                        {
                                            item1.Country = V.Country;
                                        }
                                        if (string.IsNullOrEmpty(item1.Address2))
                                        {
                                            item1.Address2 = V.Address2;
                                        }
                                        if (string.IsNullOrEmpty(item1.State))
                                        {
                                            item1.State = V.State;
                                        }
                                        if (string.IsNullOrEmpty(item1.Suburb))
                                        {
                                            item1.Suburb = V.Suburb;
                                        }
                                        if (item1.Landline != V.Landline)
                                        {
                                            item1.Landline = V.Landline;
                                        }
                                        if (string.IsNullOrEmpty(item1.CountryCode))
                                        {
                                            item1.CountryCode = V.CountryCode;
                                        }
                                        item1.UpdatedDate = DateTime.Now;

                                        updatedLeadList.Add(item1);
                                    }
                                }

                            }

                            else
                            {
                                throw new AppException("URL link is not correct");
                            }


                            _context.Leads.UpdateRange(updatedLeadList);
                            await _context.SaveChangesAsync();
                        }
                    }
                    else
                    {
                        throw new AppException("No Database associated with this show");
                    }
                }
                else
                {
                    throw new AppException("No Database associated with this show");
                }
            }
            return leads;
        }

        private async Task<GetLeadsInfoViewModel> GetLeadFromExternalAPI(Show show, string ShowKey, string barcode)
        {
            if (show.DatabaseId != null)
            {
                var database = await _context.Database.SingleOrDefaultAsync(a => a.DatabaseId == show.DatabaseId);
                if (database != null)
                {
                    var Leadslink = database.LeadsInfoURL + "?op=getLeadsInfo";
                    var client = new RestClient(Leadslink);
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("Postman-Token", "1bf33fc8-f1ce-46b2-9949-54be05c1ac9e");
                    request.AddHeader("Cache-Control", "no-cache");
                    request.AddHeader("Content-Type", "text/xml");
                    request.AddParameter("undefined", "<soap:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\">\r\n  <soap:Header>\r\n    <AuthHeader xmlns=\"http://tempuri.org/\">\r\n      <Username>" + database.UserName + "</Username>\r\n      <Password>" + database.Password + "</Password>\r\n    </AuthHeader>\r\n  </soap:Header>\r\n  <soap:Body>\r\n    <getLeadsInfo xmlns=\"http://tempuri.org/\">\r\n      <param1></param1>\r\n      <param2>" + show.DbName + "</param2>\r\n      <barcodeList>'" + barcode + "'</barcodeList>\r\n    </getLeadsInfo>\r\n  </soap:Body>\r\n</soap:Envelope>", ParameterType.RequestBody);
                    IRestResponse response = client.Execute(request);
                    if (response.IsSuccessful == true)
                    {
                        var xdoc = XDocument.Parse(response.Content);

                        var list = ((System.Xml.Linq.XElement)xdoc.FirstNode).Value.TrimStart('[').TrimStart('{').TrimEnd(']').TrimEnd('}').Split(',');

                        GetLeadsInfoViewModel externalLead = new GetLeadsInfoViewModel();
                        externalLead.Landline = "";

                        foreach (var item in list)
                        {
                            var obj = item.Split(':');

                            if (obj[0].Trim('"') == "c")
                            {
                                externalLead.FirstName = obj[1].Trim('"');
                            }
                            else if (obj[0].Trim('"') == "d")
                            {
                                externalLead.LastName = obj[1].Trim('"');
                            }
                            else if (obj[0].Trim('"') == "e")
                            {
                                externalLead.Company = obj[1].Trim('"');
                            }
                            else if (obj[0].Trim('"') == "f")
                            {
                                externalLead.Designation = obj[1].Trim('"');
                            }
                            else if (obj[0].Trim('"') == "g")
                            {
                                externalLead.Address = obj[1].Trim('"');
                            }
                            else if (obj[0].Trim('"') == "h")
                            {
                                externalLead.Address2 = obj[1].Trim('"');
                            }
                            else if (obj[0].Trim('"') == "i")
                            {
                                externalLead.Suburb = obj[1].Trim('"');
                            }
                            else if (obj[0].Trim('"') == "j")
                            {
                                externalLead.State = obj[1].Trim('"');
                            }
                            else if (obj[0].Trim('"') == "l")
                            {
                                externalLead.Country = obj[1].Trim('"');
                            }
                            else if (obj[0].Trim('"') == "m")
                            {
                                externalLead.Landline = externalLead.Landline + " " + obj[1].Trim('"');
                            }
                            else if (obj[0].Trim('"') == "n")
                            {
                                externalLead.Landline = externalLead.Landline + " " + obj[1].Trim('"');
                            }
                            else if (obj[0].Trim('"') == "o")
                            {
                                externalLead.Landline = externalLead.Landline + " " + obj[1].Trim('"');
                            }
                            else if (obj[0].Trim('"') == "k")
                            {
                                externalLead.CountryCode = obj[1].Trim('"');
                            }
                            else if (obj[0].Trim('"') == "t")
                            {
                                externalLead.Phone = obj[1].Trim('"');
                            }
                            else if (obj[0].Trim('"') == "u")
                            {
                                externalLead.Email = obj[1].Trim('"');
                            }
                        }
                        externalLead.Landline = externalLead.Landline.Replace("null", "");
                        return externalLead;


                    }
                    else
                    {
                        throw new AppException("URL link is not correct");
                    }
                }
                else
                {
                    throw new AppException("No Database associated with this show");
                }
            }
            return default(GetLeadsInfoViewModel);
        }
        public async Task UpdateQualifierAsync(Qualifier qualifier, CancellationToken ct = default(CancellationToken))
        {
            var tempQualifier = await _context.Qualifier.SingleOrDefaultAsync(a => a.QualifierId == qualifier.QualifierId && a.IsDeleted == false && a.IsActive == true);
            if (tempQualifier == null) throw new AppException("Qualifier not found");
            tempQualifier.QualifierName = qualifier.QualifierName;
            _context.Qualifier.Update(tempQualifier);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> DeleteQualifierAsync(int Id, CancellationToken ct = default(CancellationToken))
        {
            var qualifier = await _context.Qualifier.Include(a => a.Questions).SingleOrDefaultAsync(a => a.QualifierId == Id && a.IsActive == true && a.IsDeleted == false);
            if (qualifier != null)
            {
                qualifier.IsDeleted = true;
                _context.Qualifier.Update(qualifier);

                foreach (var question in qualifier.Questions)
                {
                    var deletedQuestion = await _questionRepository.DeleteQuestionAsync(question.QuestionId);
                }
                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                throw new AppException("Qualifier not found");
            }

        }
        public async Task<Qualifier> CreateQualifierWebAsync(Qualifier qualifier, int userId, CancellationToken ct = default(CancellationToken))
        {
            if (qualifier.IsAdmin == true)
            {
                //qualifier.DeviceId = null;
                if (qualifier.IsDefault.HasValue && qualifier.IsDefault.Value == true)// && qualifier.ShowId == 0)
                    qualifier.ShowId = null;
                await _context.Qualifier.AddAsync(qualifier, ct);
                await _context.SaveChangesAsync(ct);
                //if (qualifier.IsPublished == true)
                //{
                //    var realtions = await _context.QualifierUsers.Where(d => d.QualifierId == qualifier.QualifierId).ToListAsync();
                //    if (realtions == null || (realtions != null && realtions.Count <= 0))
                //    {
                //        var keys = await _context.Sduactivation.Where(a => a.ShowId == qualifier.ShowId).ToListAsync();
                //        foreach (var item in keys)
                //        {
                //            var realtion = await _context.QualifierUsers.Where(d => d.QualifierId == qualifier.QualifierId && d.UserId == item.UserId).ToListAsync();
                //            if (realtion == null || (realtion != null && realtion.Count <= 0))
                //            {
                //                QualifierUsers QU = new QualifierUsers();
                //                QU.QualifierId = qualifier.QualifierId;
                //                QU.UserId = item.UserId;
                //                var a = await _context.QualifierUsers.AddAsync(QU, ct);
                //                await _context.SaveChangesAsync(ct);
                //            }
                //        }
                //    }
                //}
                return qualifier;

                //add relation in qualifiersusertable if qualifier is publshed.
            }
            else
            {
                //qualifier.DeviceId = null;
                //qualifier.IsAdmin = false;
                await _context.Qualifier.AddAsync(qualifier, ct);
                await _context.SaveChangesAsync(ct);

                //await _context.SaveChangesAsync(ct);
                return qualifier;
            }
        }
        public async void SaveQualifierWebRelationAsync(Question question, int userId, CancellationToken ct = default(CancellationToken))

        {
            QualifierUsers QU = new QualifierUsers();
            QU.QualifierId = question.QualifierId;
            QU.UserId = userId;
            await _context.QualifierUsers.AddAsync(QU, ct);
        }
        public async Task<Qualifier> UpdateQualifierWebAsync(Qualifier qualifier, CancellationToken ct = default(CancellationToken))
        {
            var tempQualifier = await _context.Qualifier.SingleOrDefaultAsync(a => a.QualifierId == qualifier.QualifierId && a.IsDeleted == false && a.IsActive == true);
            if (tempQualifier == null) throw new AppException("Qualifier not found");
            if (qualifier.IsDefault.HasValue && qualifier.IsDefault.Value == true && qualifier.ShowId == 0)
                qualifier.ShowId = null;
            if (qualifier.IsPublished == true)
            {
                //var realtions = await _context.QualifierUsers.Where(d => d.QualifierId == tempQualifier.QualifierId).ToListAsync();
                //if (realtions == null || (realtions != null && realtions.Count <= 0))
                //{
                //    var keys = await _context.Sduactivation.Where(a => a.ShowId == qualifier.ShowId).ToListAsync();
                //    foreach (var item in keys)
                //    {
                //        var realtion = await _context.QualifierUsers.Where(d => d.QualifierId == qualifier.QualifierId && d.UserId == item.UserId).ToListAsync();
                //        if (realtion == null || (realtion != null && realtion.Count <= 0))
                //        {
                //            QualifierUsers QU = new QualifierUsers();
                //            QU.QualifierId = qualifier.QualifierId;
                //            QU.UserId = item.UserId;
                //            var a = await _context.QualifierUsers.AddAsync(QU, ct);
                //            await _context.SaveChangesAsync(ct);
                //        }
                //    }
                //}
            }
            tempQualifier.QualifierName = qualifier.QualifierName;
            tempQualifier.ShowId = qualifier.ShowId;
            tempQualifier.UpdatedDate = DateTime.Now;
            tempQualifier.IsPublished = qualifier.IsPublished;
            tempQualifier.IsDefault = qualifier.IsDefault;
            _context.Qualifier.Update(tempQualifier);
            await _context.SaveChangesAsync();

            return tempQualifier;
        }
        public async Task<QualifierUsers> DeleteQualifierUserRealtion(int qualifierId, int userid, CancellationToken ct = default(CancellationToken))
        {
            var qu = await _context.QualifierUsers.SingleOrDefaultAsync(a => a.QualifierId == qualifierId && a.UserId == userid);
            if (qu != null)
            {
                _context.QualifierUsers.Remove(qu);
                await _context.SaveChangesAsync();
                return qu;
            }
            else
            {
                QualifierUsers q = new QualifierUsers();
                return q;
            }
        }
    }
}
