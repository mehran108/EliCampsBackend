using System;
using System.Collections.Generic;

namespace ELI.Entity.Main
{
    public partial class LookupValue
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int LookupTableId { get; set; }

        public LookupTable LookupTable { get; set; }
    }
}
