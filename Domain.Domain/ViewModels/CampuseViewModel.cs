using System;
using System.Collections.Generic;
using System.Text;

namespace ELI.Domain.ViewModels
{
    public class CampuseViewModel
    {
        public int ID { get; set; }
        public string Campus { get; set; }
        public string Camps { get; set; }
        public string AddressOnReports { get; set; }
        public string CompleteName { get; set; }
        public string Onelineaddress { get; set; }
        public bool? Active { get; set; }
    }
}
