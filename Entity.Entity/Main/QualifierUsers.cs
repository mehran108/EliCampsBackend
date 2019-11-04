using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ELI.Entity.Main
{
    public partial class QualifierUsers
    {
        [Key]
        public int QualifierUserId { get; set; }
        public int? QualifierId { get; set; }
        public int? UserId { get; set; }
    }
}
