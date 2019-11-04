using System;
using System.Collections.Generic;
using System.Text;

namespace ELI.Domain.ViewModels
{
   public class SaveLeadViewModel
    {
        public string DeviceIdentifier { get; set; }
        public string ShowId { get; set; }
        public string QualifierId { get; set; }
        public string Barcode { get; set; }
        public List<QualifierDetailViewModel> QualifierDetails { get; set; }
    }
}
