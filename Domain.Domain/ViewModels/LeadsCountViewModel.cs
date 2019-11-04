using System;
using System.Collections.Generic;
using System.Text;

namespace ELI.Domain.ViewModels
{
   public class LeadsCountViewModel
    {
        public int AmountOfLeads { get; set; }
        public string StandNumber { get; set; }
        public string DeviceIdentifier { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string ActivationKey { get; set; }
        public string Company { get; set; }
        public string ShowName { get; set; }
    }
}
