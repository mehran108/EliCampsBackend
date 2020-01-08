using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace ELI.Domain.ViewModels
{
    public class EmailSendVM
    {
        public string StudentEmail { get; set; }
        public string EmailBody { get; set; }
        public int StudentID { get; set; }
        public bool IsAgentInvoice { get; set; }
        public bool IsStudentCertificate { get; set; }
        public bool IsStudentInvoice { get; set; }
    }
}
