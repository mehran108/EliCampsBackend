using ELI.Entity.Main;
using System;
using System.Collections.Generic;
using System.Text;

namespace ELI.Domain.ViewModels
{
    public class CreateQuestionViewModel
    {
        public int QuestionId { get; set; }
        public int QuestionTypeId { get; set; }
        public string QuestionTypeName { get; set; }
        public string QuestionDescription { get; set; }
        public List<OrderingOptionViewModel> options { get; set; }
        public List<QuestionOption> Responses { get; set; }

        public int QualifierId { get; set; }
        public int Sequence { get; set; }
        public string DeviceIdentifier { get; set; }
        public int? ShowId { get; set; }


    }
}
