using System;
using System.Collections.Generic;

namespace ELI.API.Main
{
    public partial class Qualifier
    {
        public int QualifierId { get; set; }
        public string QualifierName { get; set; }
        public string QualifierDescription { get; set; }
        public bool? IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsPublished { get; set; }
        public int? DeviceId { get; set; }
        public int? ShowId { get; set; }
    }
}
