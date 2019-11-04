using System;
using System.Collections.Generic;

namespace ELI.Entity.Main
{
    public partial class Pricing
    {
        public Pricing()
        {
            ShowPricing = new HashSet<ShowPricing>();
        }

        public int PricingId { get; set; }
        public string CurrencyIso { get; set; }
        public decimal Tax { get; set; }
        public decimal  KeyAmount { get; set; }
        public int? OfferQuantity { get; set; }
        public decimal? KeyAmountOffer { get; set; }
        public decimal? EquivalentKeyAmount { get; set; }
        public decimal? EquivalentKeyAmountOffer { get; set; }
        public int? RegionId { get; set; }
        public bool? IsDefault { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool? IsDeleted { get; set; }
        public decimal? KeyAmountAdditional { get; set; }
        public decimal? EquivalentKeyAmountAdditional { get; set; }
        public string TaxName { get; set; }

        public Currency CurrencyIsoNavigation { get; set; }
        public Region Region { get; set; }
        public ICollection<ShowPricing> ShowPricing { get; set; }
    }
}
