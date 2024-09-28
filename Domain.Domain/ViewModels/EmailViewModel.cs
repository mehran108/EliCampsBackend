using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace ELI.Domain.ViewModels
{
   public class EmailViewModel
    {
        public EmailViewModel()
        {
            emailAttachment = new List<Attachment>();
        }

        #region Propeties
        public string RequesterEmail { get; set; }
        public string RequesterName { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public string To { get; set; }
        public string CC { get; set; }
        public string BCC { get; set; }
        public string From { get; set; }
        public bool HasAttachment { get; set; }
        public string AgentInvoiceTemplate { get; set; }
        public string StudentInvoiceTemplate { get; set; }
        public string StudentCertificateTemplate { get; set; }
        public string AirportInvoiceTemplate { get; set; }
        public string LOAInvoiceTemplate { get; set; }
        public string StudentInvitationTemplate { get; set; }
        public string LOAGroupInvoiceTemplate { get; set; }
        public string LOAInvoiceWOPTemplate { get; set; }
        public List<Attachment> emailAttachment { get; set; }
        #endregion
    }
}
