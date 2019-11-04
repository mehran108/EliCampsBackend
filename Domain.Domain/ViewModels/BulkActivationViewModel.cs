using System;
using System.Collections.Generic;
using System.Text;

namespace ELI.Domain.ViewModels
{
   public class BulkActivationViewModel
    {
        public string FirstName { get; set; }
        public string SurName { get; set; }
        public string Email { get; set; }
        public string Company { get; set; }
        public string StreetAddress { get; set; }
        public string Suburb { get; set; }
        public string Country { get; set; }
        public string State_Province { get; set; }
        public string PhoneNumber { get; set; }
        public string ShowKey { get; set; }
        public int Quantity { get; set; }
    }
}
