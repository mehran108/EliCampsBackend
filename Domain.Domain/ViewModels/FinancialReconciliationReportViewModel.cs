using System;
using System.Collections.Generic;
using System.Text;

namespace ELI.Domain.ViewModels
{
   public class FinancialReconciliationReportViewModel
    {
        public int? InvoiceId { get; set; }
        public string UnlockCode { get; set; }
        public string FirstName { get; set; }
        public string SurName { get; set; }
        public string Company { get; set; }
        public DateTime? Purchased { get; set; }
        public bool? Used { get; set; }
        public string ShowName { get; set; }
        public DateTime? Unlocked { get; set; }
        public decimal? KeyPrice { get; set; }
    }
}
