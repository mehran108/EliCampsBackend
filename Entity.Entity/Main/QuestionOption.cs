using System;
using System.Collections.Generic;

namespace ELI.Entity.Main
{
    public partial class QuestionOption
    {
        public int QuestionOptionId { get; set; }
        public int QuestionId { get; set; }
        public string option { get; set; }
        public bool? IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool? IsDeleted { get; set; }
        public Question Question { get; set; }
        public int Sequence { get; set; }

    }
}
