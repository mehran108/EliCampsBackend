using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ELI.Domain.Helpers;
using ELI.Domain.ViewModels;

namespace ELI.Domain.Services
{
    public interface IEmailSender
    {
        ResetPasswordViewModel GeneratePasswordResetTokenAsync();
        Task SendEmaill(string code, string Email, EmailTemplate emailTemplateId);
        Task<bool> SendRegistrationEmailWithDocument(EmailSendVM emailSendVM);
    }
}
