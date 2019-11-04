using ELI.Entity.Auth;
using System;
using System.Collections.Generic;

namespace ELI.Entity.Main
{
    public  class Invoice
    {
        public int InvoiceId { get; set; }
        public decimal? Total { get; set; }
        public decimal? SubTotal { get; set; }
        public decimal? Discount { get; set; }
        public decimal? Tax { get; set; }
        public int? Quantity { get; set; }
        public int? ShowId { get; set; }
        public int? UserId { get; set; }
        public decimal? KeyPrice { get; set; }
        public string PaymentId { get; set; }
        public string RequestXml { get; set; }
        public string ResponseXml { get; set; }
        public string DiscountCode { get; set; }
        public string RestrictedCode { get; set; }
        public bool? IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool? IsDeleted { get; set; }
        public Show Show { get; set; }
        public bool IsSuccess { get; set; }
        public Users user { get; set; }
    }
}
