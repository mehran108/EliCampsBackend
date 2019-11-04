using System;
using System.Collections.Generic;

namespace ELI.Entity.Main
{
    public partial class LeadsQualifier
    {
        public int LeadsQualifierId { get; set; }
        public int? LeadsId { get; set; }
        public int? Sduid { get; set; }
        public int? QualifierId { get; set; }
        public int? QuestionId { get; set; }
        public string Response { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsActive { get; set; }

        public Leads Leads { get; set; }
    }
}
