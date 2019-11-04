using System;
using System.Collections.Generic;

namespace ELI.Entity.Main
{
    public partial class LookupTable
    {
        public LookupTable()
        {
            LookupValue = new HashSet<LookupValue>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<LookupValue> LookupValue { get; set; }
    }
}
