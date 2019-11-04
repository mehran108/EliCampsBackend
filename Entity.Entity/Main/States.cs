using System;
using System.Collections.Generic;

namespace ELI.Entity.Main
{
    public partial class States
    {
        public States()
        {
            Cities = new HashSet<Cities>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int CountryId { get; set; }

        public Countries Country { get; set; }
        public ICollection<Cities> Cities { get; set; }
    }
}
