using System;
using System.Collections.Generic;
using System.Text;

namespace ELI.Domain.ViewModels
{
   public class CodeListReportViewModel
    {
        public string State_Province { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public decimal? Total { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime? Purchased { get; set; }
        public string Code { get; set; }
        public bool? Used { get; set; }
        public string ShowName { get; set; }
        public int Leads { get; set; }
        public decimal? KeyPrice { get; set; }
        public string Company { get; set; }
        public string FirstName { get; set; }
        public string SurName { get; set; }
    }
}
