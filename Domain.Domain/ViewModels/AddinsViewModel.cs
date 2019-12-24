using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Domain.ViewModels
{
    public class AddinsViewModel
    {
        public int ID { get; set; }
        public string Addins { get; set; }
        public string Camps { get; set; }
        public decimal? Cost { get; set; }
        public string AddinsType { get; set; }
        public bool Active { get; set; }
        public bool IsDefault { get; set; }
    }
}
