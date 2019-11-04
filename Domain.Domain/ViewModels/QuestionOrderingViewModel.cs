using ELI.Entity.Main;
using System;
using System.Collections.Generic;
using System.Text;

namespace ELI.Domain.ViewModels
{
    public class QuestionOrderingViewModel
    {
        public int QualifierId { get; set; }
        public List<OrderQuestionViewModel> Questions { get; set; }
        string DeviceIdentifier;
    }
}
