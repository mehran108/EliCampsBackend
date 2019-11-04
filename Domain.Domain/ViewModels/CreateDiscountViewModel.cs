using System;
using System.Collections.Generic;
using System.Text;

namespace ELI.Domain.ViewModels
{
  public  class CreateDiscountViewModel
    {
        public int DiscountId { get; set; }
        public string DiscountCode { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public int? MinimumBuy { get; set; }
        public decimal? DiscountValue { get; set; }
        public int? DiscountType { get; set; }
        public bool? IsConsumed { get; set; }

    }
}
