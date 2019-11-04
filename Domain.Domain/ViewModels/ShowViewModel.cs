using System;
using System.Collections.Generic;
using System.Text;

namespace ELI.Domain.ViewModels
{
    public class ShowViewModel
    {
        public int ShowId { get; set; }
        public string ShowKey { get; set; }
        public string ShowName { get; set; }
        public string ShowShortName { get; set; }
        public bool? IsNFC { get; set; }
        public bool? IsActive { get; set; }
        public List<int> DiscountCodes { get; set; }
        public string Location { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Description { get; set; }
        public int? DatabaseId { get; set; }
        public string BarcodePrefix { get; set; }
        public bool? IsRestricted { get; set; }
        public decimal KeyAmount { get; set; }
        public string TaxName { get; set; }
        public decimal Tax { get; set; }
        public int PaymentGatewayId { get; set; }
        public string OnsiteHelpNumber { get; set; }
        public string Logo { get; set; }
        public string ImagePath { get; set; }
        public decimal LeadsDownloadLimit { get; set; }
        public decimal LeadsSequentialLimit { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string DbName { get; set; }


    }
}
