using System;
using System.Collections.Generic;

namespace ELI.Entity.Main
{
    public partial class ErrorLogging
    {
        public int ErrorId { get; set; }
        public string Level { get; set; }
        public string Message { get; set; }
        public string App { get; set; }
        public string TimeStamp { get; set; }
    }
}
