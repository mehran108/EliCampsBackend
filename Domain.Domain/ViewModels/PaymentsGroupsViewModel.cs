using System;
using System.Collections.Generic;
using System.Text;

namespace ELI.Domain.ViewModels
{
    public class PaymentsGroupsViewModel
    {
        public int ID { get; set; }
        public string RefNumber { get; set; }
        public int GroupID { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string Remarks { get; set; }
        public bool Active { get; set; }
    }
}
