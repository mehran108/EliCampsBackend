using System;
using System.Collections.Generic;
using System.Text;

namespace ELI.Domain.ViewModels
{
   public class DiscountViewModel
    {
        public int DiscountId { get; set; }
        public string DiscountCode { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public int? MinimumBuy { get; set; }
        public decimal? DiscountValue { get; set; }
        public int? DiscountType { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsConsumed { get; set; }
        public int TimesUsed { get; set; }
    }
}
