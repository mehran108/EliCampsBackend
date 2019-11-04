using System;
using System.Collections.Generic;

namespace ELI.Entity.Main
{
    public partial class ShowDiscount
    {
        public int ShowDiscountId { get; set; }
        public int? ShowId { get; set; }
        public int? DiscountId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsActive { get; set; }
    }
}
