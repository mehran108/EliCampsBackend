using System;
using System.Collections.Generic;
using System.Text;

namespace ELI.Domain.ViewModels
{
   public class CreateInvoiceViewModel
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
    }
}
