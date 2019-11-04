using System;
using System.Collections.Generic;
using System.Text;

namespace ELI.Domain.ViewModels
{
  public class GetAllLeadsFromDevice
    {
        public int LeadsId { get; set; }
        public int? QualifierId { get; set; }
        public int? QuestionId { get; set; }
        public List<Entity.Main.LeadsQualifier> Response { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string DeviceIdentifier { get; set; }
        public string ShowId { get; set; }
        public string Barcode { get; set; }
        public int? Sduid { get; set; }
        public string FullName { get; set; }
    }
}
