using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Domain.ViewModels
{
    public class TripsViewModel
    {
        public int ID { get; set; }
        public int Year { get; set; }
        public string Trips { get; set; }
        public string Camps{ get; set; }
        public DateTime TripsDate { get; set; }
        public string Notes { get; set; }
        public string ldx { get; set; }
        public bool Active { get; set; }
    }
}
