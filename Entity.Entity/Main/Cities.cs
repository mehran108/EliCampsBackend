using System;
using System.Collections.Generic;

namespace ELI.Entity.Main
{
    public partial class Cities
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int StateId { get; set; }

        public States State { get; set; }
    }
}
