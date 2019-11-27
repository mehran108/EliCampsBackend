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
        public int? AgentID { get; set; }
        public string AgentName { get; set; }
        public string AgencyRef { get; set; }
        public string Country { get; set; }
        public string InvoiceType { get; set; }
        public DateTime? ArrivalDate { get; set; }
        public string Terminal { get; set; }
        public string FlightNumber { get; set; }
        public string DestinationFrom { get; set; }
        public string ArrivalTime { get; set; }
        public DateTime? DepartureDate { get; set; }
        public string DepartureTerminal { get; set; }
        public string DepartureFlightNumber { get; set; }
        public string DestinationTo { get; set; }
        public string FlightDepartureTime { get; set; }
        public DateTime? ProgrameStartDate { get; set; }
        public DateTime? ProgrameEndDate { get; set; }
        public int? Campus { get; set; }
        public string CampusName { get; set; }
        public int? Format { get; set; }
        public string FormatName { get; set; }
        public string MealPlan { get; set; }
        public List<int> ProgrameAddins { get; set; }
        public string AddinsID { get; set; }
        public List<int> GroupTrips { get; set; }
        public string GroupTripsID { get; set; }
        public int NumberOfNights { get; set; }
        public decimal TotalGrossPrice { get; set; }
        public decimal Paid { get; set; }
        public decimal Commision { get; set; }
        public decimal NetPrice { get; set; }
        public decimal Balance { get; set; }
        public int NumOfStudents { get; set; }
        public int NumOfGrpLeaders { get; set; }
        public decimal PerStudent { get; set; }
        public decimal PerGrpLeader { get; set; }

        public bool ApplyToAllStudent { get; set; }

        public bool? Active { get; set; }

        public string ChapFamily { get; set; }
        public int? ProgramId { get; set; }
        public int? SubProgramId { get; set; }
    }
}
