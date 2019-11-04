using System;
using System.Collections.Generic;

namespace ELI.Entity.Main
{
    public partial class QuestionType
    {
        public QuestionType()
        {
            Question = new HashSet<Question>();
        }

        public int QuestionTypeId { get; set; }
        public string QuestionTypeName { get; set; }
        public string QuestionTypeDescription { get; set; }
        public bool? IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool? IsDeleted { get; set; }

        public ICollection<Question> Question { get; set; }
    }
}
