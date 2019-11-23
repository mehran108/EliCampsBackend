using System;
using System.Collections.Generic;
using System.Text;

namespace ELI.Domain.ViewModels
{
    public class SubProgramViewModel
    {
        public int ID { get; set; }
        public int ProgramID { get; set; }
        public string SubProgramName { get; set; }
        public string ProgramName { get; set; }
        public bool? Active { get; set; }
    }
}
