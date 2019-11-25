using System;
using System.Collections.Generic;
using System.Text;

namespace ELI.Domain.ViewModels
{
    public class PaymentsViewModel
    {
        public int ID { get; set; }
        public int StudentRegID { get; set; }
        public DateTime? Date { get; set; }
        public decimal Amount { get; set; }
        public string Remarks { get; set; }
        public bool? Active { get; set; }
    }
}
