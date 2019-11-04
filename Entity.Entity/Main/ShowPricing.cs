using System;
using System.Collections.Generic;

namespace ELI.Entity.Main
{
    public partial class ShowPricing
    {
        public int ShowPricingId { get; set; }
        public int? ShowId { get; set; }
        public int? PricingId { get; set; }
        public bool? IsDeleted { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public Pricing Pricing { get; set; }
        public Show Show { get; set; }
    }
}
