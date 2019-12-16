using System;
using System.Collections.Generic;
using System.Text;

namespace ELI.Domain.ViewModels
{
    public class StudentRegistration
    {
        public int ID { get; set; }
        public int Year { get; set; }
        public string Reg_Ref { get; set; }
        public string GroupRef { get; set; }
        public string Camps { get; set; }
        public string Gender { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string HomeAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PostCode { get; set; }
        public string EmergencyContact { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime? DOB { get; set; }
        public int? Age { get; set; }
        public string PassportNumber { get; set; }
        public int? AgencyID { get; set; }
        public DateTime? ArrivalDate { get; set; }
        public string Terminal { get; set; }
        public string FlightNumber { get; set; }
        public string DestinationFrom { get; set; }
        public DateTime? ArrivalTime { get; set; }
        public DateTime? DepartureDate { get; set; }
        public string DepartureTerminal { get; set; }
        public string DepartureFlightNumber { get; set; }
        public string DestinationTo { get; set; }
        public DateTime? FlightDepartureTime { get; set; }
        public string MedicalInformation { get; set; }
        public string DietaryNeeds { get; set; }
        public string Allergies { get; set; }
        public string MedicalNotes { get; set; }
        public DateTime? ProgrameStartDate { get; set; }
        public DateTime? ProgrameEndDate { get; set; }
        public int? Campus { get; set; }
        public int? Format { get; set; }
        public string MealPlan { get; set; }
        public List<int> ProgrameAddins { get; set; }
        public string AddinsID { get; set; }
        public string ExtraNotes { get; set; }
        public string ExtraNotesHTML { get; set; }
        public string Status { get; set; }

        public string HomestayOrResi { get; set; }
        public int? HomestayID { get; set; }
        public int? RoomID { get; set; }
        public int? RoomSearchCampus { get; set; }
        public DateTime? RoomSearchFrom { get; set; }
        public DateTime? RoomSearchTo { get; set; }
        public int NumberOfNights { get; set; }
        public double TotalGrossPrice { get; set; }
        public double TotalAddins { get; set; }
        public double Paid { get; set; }
        public double Commision { get; set; }
        public double CommissionAddins { get; set; }
        public string ProfilePic { get; set; }
        public double NetPrice { get; set; }
        public double Balance { get; set; }
        public List<int> StudentTrips { get; set; }
        public string StudentTripsID { get; set; }
        public string FormatName { get; set; }
        public string AgentName { get; set; }
        public bool? Active { get; set; }
        public string ChapFamily { get; set; }
        public int? ProgramID { get; set; }
        public int? SubProgramID { get; set; }
        public int DocumentId { get; set; }
        public string DocumentPath { get; set; }




    }
}
