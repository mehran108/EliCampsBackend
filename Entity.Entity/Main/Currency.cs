using System;
using System.Collections.Generic;

namespace ELI.Entity.Main
{
    public partial class Currency
    {
        public Currency()
        {
            Pricing = new HashSet<Pricing>();
            Region = new HashSet<Region>();
        }

        public string Iso { get; set; }
        public string Name { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool? IsDeleted { get; set; }
        public int? CurrencyId { get; set; }

        public ICollection<Pricing> Pricing { get; set; }
        public ICollection<Region> Region { get; set; }
    }
}
