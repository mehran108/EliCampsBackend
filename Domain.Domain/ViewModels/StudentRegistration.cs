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
        public DateTime DOB { get; set; }
        public int Age { get; set; }
        public string PassportNumber { get; set; }
        public int AgencyID { get; set; }
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
        public string MedicalInformation { get; set; }
        public string DietaryNeeds { get; set; }
        public string Allergies { get; set; }
        public string MedicalNotes { get; set; }
        public DateTime ProgrameStartDate { get; set; }
        public DateTime ProgrameEndDate { get; set; }
        public int Campus { get; set; }
        public int Format { get; set; }
        public string MealPlan { get; set; }
        public List<int> ProgrameAddins { get; set; }
        public string AddinsID { get; set; }
        public string ExtraNotes { get; set; }


    }
}
