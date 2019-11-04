using System;
using System.Collections.Generic;

namespace ELI.Entity.Main
{
    public partial class SynchronizationLog
    {
        public int SynchronizationLogId { get; set; }
        public string SynchronizationLogName { get; set; }
        public string AttributeName { get; set; }
        public bool? IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
