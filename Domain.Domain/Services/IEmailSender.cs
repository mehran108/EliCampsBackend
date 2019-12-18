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
        Task SendEmail(string code, string Email, EmailTemplate emailTemplateId);
        Task<bool> SendRegistrationEmail(List<Documents> document);
    }
}
