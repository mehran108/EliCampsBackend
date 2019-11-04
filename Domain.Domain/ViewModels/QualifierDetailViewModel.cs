using System;
using System.Collections.Generic;
using System.Text;

namespace ELI.Domain.ViewModels
{
   public class QualifierDetailViewModel
    {
        public string QuestionId { get; set; }
        public List<string> Response { get; set; }
        //public bool Toggle { get; set; }
        //public KeyValuePair<string,bool> CheckBox { get; set; }
    }
}
