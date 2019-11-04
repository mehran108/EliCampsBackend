using System;
using System.Collections.Generic;

namespace ELI.Entity.Main
{
    public partial class Show
    {
        public Show()
        {
            ShowPricing = new HashSet<ShowPricing>();
        }

        public int ShowId { get; set; }
        public string ShowKey { get; set; }
        public int? ShowCode { get; set; }
        public string ShowName { get; set; }
        public string DbName { get; set; }
        public int? RegionId { get; set; }
        public string Location { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Description { get; set; }
        public int? DatabaseId { get; set; }
        public string PreRegCode { get; set; }
        public bool? IsReportsAvailable { get; set; }
        public bool? IsActive { get; set; }
        public string Message { get; set; }
        public string BarcodePrefix { get; set; }
        public bool? IsRestricted { get; set; }
        public bool? IsShowCompleted { get; set; }
        public string OnsiteHelpNumber { get; set; }
        public bool? IsQualifierIncluded { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool? IsDeleted { get; set; }
        public int? PaymentGatewayId { get; set; }
        public string Logo { get; set; }
        public string ImagePath { get; set; }

        public string ShowShortName { get; set; }
        public bool? IsNfc { get; set; }
        public decimal? LeadsDownloadLimit { get; set; }
        public decimal? LeadsSequentialLimit { get; set; }
        //public int? DiscountId { get; set; }
        public Discount Discount { get; set; }


        public Region Region { get; set; }
        public ICollection<ShowPricing> ShowPricing { get; set; }

    }
}
