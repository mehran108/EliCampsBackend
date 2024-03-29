﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ELI.Domain.ViewModels
{
    public class ProgramViewModel
    {
        public int ID { get; set; }
        public string ProgramName { get; set; }
        public bool? Active { get; set; }
        public bool IsDefault { get; set; }
    }
}
