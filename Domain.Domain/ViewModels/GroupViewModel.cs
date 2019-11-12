using System;
using System.Collections.Generic;
using System.Text;

namespace ELI.Domain.ViewModels
{
    public class GroupViewModel
    {
        public int ID { get; set; }
        public int Year { get; set; }
        public string Camps { get; set; }
        public string RefNumber { get; set; }
        public int AgentID { get; set; }
        public string AgencyRef { get; set; }
        public string Country { get; set; }
        public DateTime ArrivalDate { get; set; }
        public string Terminal { get; set; }
        public string FlightNumber { get; set; }
        public string DestinationFrom { get; set; }
        public string ArrivalTime { get; set; }
        public DateTime DepartureDate { get; set; }
        public string DepartureTerminal { get; set; }
        public string DepartureFlightNumber { get; set; }
        public string DestinationTo { get; set; }
        public string FlightDepartureTime { get; set; }
        public bool Active { get; set; }
    }
}
