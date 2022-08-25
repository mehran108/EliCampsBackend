using ELI.Domain.Helpers;
using ELI.Domain.ViewModels;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ELI.Domain.Services
{
    public interface IEmailSender
    {
        ResetPasswordViewModel GeneratePasswordResetTokenAsync();
        Task SendEmaill(string code, string Email, EmailTemplate emailTemplateId);
        Task<bool> SendRegistrationEmailWithDocument(EmailSendVM emailSendVM);
        Task<MemoryStream> DocumentGet(EmailSendVM emailSendVM);
        Task<MemoryStream> GetCertificate(string studentName);
    }
}
