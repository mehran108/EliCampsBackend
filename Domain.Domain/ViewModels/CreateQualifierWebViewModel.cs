using System;
using System.Collections.Generic;
using System.Text;

namespace ELI.Domain.ViewModels
{
  public  class CreateQualifierWebViewModel 
    {
        public int ShowId { get; set; }
        public int UserId { get; set; }
        public int QualifierId { get; set; }
        public string QualifierName { get; set; }
        public bool IsPublished { get; set; }
        public bool IsDefault { get; set; }
        public bool IsAdmin { get; set; }
        public string OwnerId { get; set; }
        public List<CreateQuestionWebViewModel> Questions { get; set; }
        public List<int> DeletedQuestions { get; set; }
    }
}
