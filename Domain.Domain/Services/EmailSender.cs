using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;
using ELI.Domain.ViewModels;
using ELI.Entity.Main;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ELI.Domain.Helpers;
using ELI.Domain.Contracts.Auth;
using ELI.Domain.Contracts.Main;
using System.Net.Mail;
using System.Net;

namespace ELI.Domain.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly IELIService _ELIService;
        private IConfiguration _configuration { get; }
        private IUserRepository _userRepository { get; }
        private ILookupTableRepository _lookupTableRepository { get; }
        string RegionId;
        string AuthConString;
        string ConString;
        string DefaultPassword;



        public EmailSender(IELIService ELISupervisor, IConfiguration iconfiguration, IUserRepository iuserRepository,
            ILookupTableRepository ilookupTableRepository)
        {
            _ELIService = ELISupervisor;
            _configuration = iconfiguration;
            _userRepository = iuserRepository;
            _lookupTableRepository = ilookupTableRepository;
            var builder = new ConfigurationBuilder()
           .AddJsonFile("appsettings.json")
           .AddJsonFile($"appsettings.json", true)
           .AddEnvironmentVariables();
            var config = builder.Build();
            ConString = config.GetConnectionString("ELIDb");
            AuthConString = config.GetConnectionString("ELIAuthDb");
            RegionId = config.GetConnectionString("RegionId");
            DefaultPassword = config.GetSection("ConnectionStrings").GetSection("DefaultPassword").Value;

        }
        public async Task SendEmail(string SecurityKey, string Email, EmailTemplate emailTemplateId)
        {
            var region = await _ELIService.GetRegionById(Convert.ToInt32(RegionId));

            try
            {


                if (emailTemplateId == EmailTemplate.Welcome)
                {
                    var emailTemplate = GetEmailTemplate(emailTemplateId, region.RegionName);
                    var user = await _userRepository.GetByEmailAsync(Email);
                    var imgSourceLink = _lookupTableRepository.getpath(LookupValueEnum.ImgSrcPath);
                    var CompanyTel = _lookupTableRepository.getpath(LookupValueEnum.CompanyTel);
                    var CompanyAddress = _lookupTableRepository.getpath(LookupValueEnum.CompanyAddress);
                    var regionUrl = _lookupTableRepository.getpath(LookupValueEnum.region_url);

                    var clientKey = _configuration.GetSection("Data").GetSection("ClientKey").Value;
                    var FromEmail = _configuration.GetSection("Data").GetSection("FromEmail").Value;
                    var Region = _configuration.GetSection("Data").GetSection("Region").Value;
                    var client = new SendGridClient(apiKey: clientKey);
                    var from = new EmailAddress(FromEmail, "Info Tracker");
                    var subject = emailTemplate.Subject.ToString();
                    var to = new EmailAddress(Email, "User to recover");
                    var plainTextContent = emailTemplate.Body;
                    //var htmlContent = emailTemplate.Body;
                    CompanyAddress.Description = CompanyAddress.Description.Replace("\n", "<br>");
                    var htmlContent = String.Format(emailTemplate.Body, user.FirstName, regionUrl.Description, CompanyTel.Description, CompanyAddress.Description, imgSourceLink.Description);
                    var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
                    var response = await client.SendEmailAsync(msg);
                }
                else if (emailTemplateId == EmailTemplate.ForgetPasswordEmail)
                {
                    var emailTemplate = GetEmailTemplate(emailTemplateId, region.RegionName);
                    var imgSourceLink = _lookupTableRepository.getpath(LookupValueEnum.ImgSrcPath);
                    var resetUrl = _lookupTableRepository.getpath(LookupValueEnum.ResetUrl);
                    resetUrl.Description = resetUrl.Description + "?Key=" + SecurityKey + "&email=" + Email;
                    var CompanyAddress = _lookupTableRepository.getpath(LookupValueEnum.CompanyAddress);
                    var user = await _userRepository.GetByEmailAsync(Email);
                    var clientKey = _configuration.GetSection("Data").GetSection("ClientKey").Value;
                    var FromEmail = _configuration.GetSection("Data").GetSection("FromEmail").Value;
                    var Region = _configuration.GetSection("Data").GetSection("Region").Value;
                    var client = new SendGridClient(apiKey: clientKey);
                    var from = new EmailAddress(FromEmail, "Info Tracker");
                    var subject = emailTemplate.Subject.ToString();
                    var to = new EmailAddress(Email, "User to recover");
                    var plainTextContent = emailTemplate.Body;
                    //var htmlContent = emailTemplate.Body;
                    CompanyAddress.Description = CompanyAddress.Description.Replace("\n", "<br>");
                    var htmlContent = String.Format(emailTemplate.Body, user.FirstName, resetUrl.Description, CompanyAddress.Description, imgSourceLink.Description);
                    var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
                    var response = await client.SendEmailAsync(msg);
                }
                else if (emailTemplateId == EmailTemplate.ResetSuccessfully)
                {
                    var emailTemplate = GetEmailTemplate(emailTemplateId, region.RegionName);
                    var imgSourceLink = _lookupTableRepository.getpath(LookupValueEnum.ImgSrcPath);
                    var CompanyAddress = _lookupTableRepository.getpath(LookupValueEnum.CompanyAddress);
                    var user = await _userRepository.GetByEmailAsync(Email);
                    var clientKey = _configuration.GetSection("Data").GetSection("ClientKey").Value;
                    var FromEmail = _configuration.GetSection("Data").GetSection("FromEmail").Value;
                    var Region = _configuration.GetSection("Data").GetSection("Region").Value;
                    var client = new SendGridClient(apiKey: clientKey);
                    var from = new EmailAddress(FromEmail, "Info Tracker");
                    var subject = emailTemplate.Subject.ToString();
                    var to = new EmailAddress(Email, "User to recover");
                    var plainTextContent = emailTemplate.Body;
                    //var htmlContent = emailTemplate.Body;
                    CompanyAddress.Description = CompanyAddress.Description.Replace("\n", "<br>");
                    var htmlContent = String.Format(emailTemplate.Body, user.FirstName, CompanyAddress.Description, imgSourceLink.Description);
                    var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
                    var response = await client.SendEmailAsync(msg);
                }
                else if (emailTemplateId == EmailTemplate.BulkCodeAccount)
                {
                    var emailTemplate = GetEmailTemplate(emailTemplateId, region.RegionName);
                    var user = await _userRepository.GetByEmailAsync(Email);
                    var imgSourceLink = _lookupTableRepository.getpath(LookupValueEnum.ImgSrcPath);
                    var CompanyTel = _lookupTableRepository.getpath(LookupValueEnum.CompanyTel);
                    var CompanyAddress = _lookupTableRepository.getpath(LookupValueEnum.CompanyAddress);
                    var regionUrl = _lookupTableRepository.getpath(LookupValueEnum.region_url);
                    var clientKey = _configuration.GetSection("Data").GetSection("ClientKey").Value;
                    var FromEmail = _configuration.GetSection("Data").GetSection("FromEmail").Value;
                    var Region = _configuration.GetSection("Data").GetSection("Region").Value;
                    var client = new SendGridClient(apiKey: clientKey);
                    var from = new EmailAddress(FromEmail, "Info Tracker");
                    var subject = emailTemplate.Subject.ToString();
                    var to = new EmailAddress(Email, "User to recover");
                    var plainTextContent = emailTemplate.Body;
                    //var htmlContent = emailTemplate.Body;
                    CompanyAddress.Description = CompanyAddress.Description.Replace("\n", "<br>");
                    var htmlContent = String.Format(emailTemplate.Body, user.FirstName, regionUrl.Description, CompanyTel.Description, CompanyAddress.Description, imgSourceLink.Description, DefaultPassword);
                    var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
                    var response = await client.SendEmailAsync(msg);
                }

            }
            catch (AppException ex)
            {
                new ExceptionHandlingService(ex, null, null).LogException();
                return;
            }

        }
        public EmailTemplateViewModel GetEmailTemplate(EmailTemplate Email_templateId, string RegionName)
        {
            EmailTemplateViewModel da = new EmailTemplateViewModel();

            DataTable dt = new DataTable();
            List<EmailTemplateViewModel> dal = new List<EmailTemplateViewModel>();
            using (SqlConnection sqlConn = new SqlConnection(ConString))
            {
                string sql = "spTemplateSelection";
                using (SqlCommand sqlCmd = new SqlCommand(sql, sqlConn))
                {
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    //sqlCmd.Parameters.AddWithValue("@regionname,", RegionName);
                    sqlCmd.Parameters.Add("@regionname", SqlDbType.NVarChar);
                    sqlCmd.Parameters["@regionname"].Value = RegionName;
                    sqlCmd.Parameters.Add("@TemplateTypeid", SqlDbType.Int);
                    sqlCmd.Parameters["@TemplateTypeid"].Value = Email_templateId;
                    //sqlCmd.Parameters.AddWithValue("@TemplateTypeid,", TemplateTypeId);

                    sqlConn.Open();
                    using (SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCmd))
                    {

                        sqlAdapter.Fill(dt);
                    }
                }
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                da.Body = dt.Rows[i]["Body"].ToString();
                da.RegionId = dt.Rows[i]["RegionId"].ToString();
                da.Subject = dt.Rows[i]["Subject"].ToString();
                da.TemplateType = dt.Rows[i]["Name"].ToString();
                // dal.Add(da);
                //return da;
            }
            return da;
        }
        public ResetPasswordViewModel GeneratePasswordResetTokenAsync()
        {
            ResetPasswordViewModel resetPasswordViewModel = new ResetPasswordViewModel
            {
                SecurityStamp = Guid.NewGuid().ToString(),
                // TempPassword = Guid.NewGuid().ToString()
            };
            return resetPasswordViewModel;
        }


        public async Task<bool> SendRegistrationEmail(List<Documents> document)
        {
            // Common method for getting template from database based email template code
            //var emailTemplate = await GetNotificationEmailTemplate(EmailApplication.FleetSalesCode);
            var emailTemplate = "My name is Zaki";
            var response = false;

            var email = new EmailViewModel();
            email.Subject = "Test Email";
            email.Message = emailTemplate; ;
            email.To = "Zulqarnain.SaleemBajwa@gmail.com";
            await sendEmail(document);
            response = true;

            return response;
        }

        private async Task sendEmail(List<Documents> document)
        {


            try
            {




                var apiKey = "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";
                var client = new SendGridClient(apiKey);
                var from = new EmailAddress("elicampswork@gmail.com", "elicamps");
                var subject = "Sending with Twilio SendGrid is Fun";
                var to = new EmailAddress("Zulqarnain.SaleemBajwa@gmail.com", "Zulqarnain");
                var plainTextContent = "";
                var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";

                var attachments = new List<SendGrid.Helpers.Mail.Attachment>();
                

                foreach(var attchment in document)
                {
                    attachments.Add(new SendGrid.Helpers.Mail.Attachment()
                    {
                        Content = Convert.ToBase64String(attchment.DocumentByte, 0, attchment.DocumentByte.Length),
                        Type = "application/pdf",
                        Filename = attchment.DocumentName,
                        Disposition = "attachment"
                    }
                        );
                }
               
                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
                msg.AddAttachments(attachments);

                var response = await client.SendEmailAsync(msg);

            }
            catch (AppException ex)
            {
                new ExceptionHandlingService(ex, null, null).LogException();
                return;
            }


        }

        // public 
    }
}
