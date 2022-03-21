using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace ELI.Domain.ViewModels
{
    public class EmailSendVM
    {
        public string StudentEmail { get; set; }
        public int StudentID { get; set; }
        public bool IsAgentInvoice { get; set; }
        public bool IsStudentCertificate { get; set; }
        public bool IsStudentInvoice { get; set; }
        public bool IsAirportInvoice { get; set; }
        public bool IsLoaInvoice { get; set; }
        public bool IsLoaGroupInvoice { get; set; }
        public bool IsLoaInvoiceWithNoPrice { get; set; }
        public bool IsStudentInvitation { get; set; }
        public double RegistrationFee { get; set; }
    }
}
