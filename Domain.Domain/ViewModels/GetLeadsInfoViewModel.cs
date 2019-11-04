using System;
using System.Collections.Generic;
using System.Text;

namespace ELI.Domain.ViewModels
{
   public class GetLeadsInfoViewModel
    {
        public string BarCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Company { get; set; }
        public string Designation { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        public string State { get; set; }
        public string Suburb { get; set; }
        public string Country { get; set; }
        public string Landline { get; set; }
        public string CountryCode { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}
