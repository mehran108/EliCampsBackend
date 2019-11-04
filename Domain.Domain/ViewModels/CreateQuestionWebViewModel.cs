using System;
using System.Collections.Generic;
using System.Text;

namespace ELI.Domain.ViewModels
{
   public class CreateQuestionWebViewModel
    {
        public int QuestionId { get; set; }
        public int QuestionTypeId { get; set; }
        public string QuestionDescription { get; set; }
        public List<OrderingOptionViewModel> options { get; set; }
        public int QualifierId { get; set; }
        public int Sequence { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsExhibitor { get; set; }
        public string OwnerId { get; set; }

    }
}
