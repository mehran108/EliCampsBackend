using System;
using System.Collections.Generic;

namespace ELI.Entity.Main
{
    public partial class Question
    {
        public int QuestionId { get; set; }
        public int QuestionTypeId { get; set; }
        public string QuestionTypeName { get; set; }
        public string QuestionDescription { get; set; }
        public bool? IsActive { get; set; }
        public string CreatedBy { get; set; }
        public string OwnerId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool? IsDeleted { get; set; }
        public int QualifierId { get; set; }
        public Qualifier Qualifier { get; set; }
        public List<QuestionOption> Responses { get; set; }
        public int Sequence { get; set; }
        public QuestionType QuestionType { get; set; }
        public bool? IsAdmin { get; set; }
        public bool? IsExhibitor { get; set; }
        public int? DeviceId { get; set; }
        public int? ShowId { get; set; }

    }
}
