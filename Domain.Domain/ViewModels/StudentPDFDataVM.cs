using System;
using System.Collections.Generic;
using System.Text;

namespace ELI.Domain.ViewModels
{
    public class StudentPDFDataVM
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AgentName { get; set; }
        public string AgentAddress { get; set; }
        public string AgentCountry { get; set; }
        public string Reg_Ref { get; set; }
        public string CampusAddressOnReports { get; set; }
        public DateTime? DOB { get; set; }
        public DateTime? ProgrameStartDate { get; set; }
        public DateTime? ProgrameEndDate { get; set; }
        public DateTime? ArrivalDate { get; set; }
        public DateTime? ArrivalTime { get; set; }
        public string ProgramName { get; set; }
        public string FlightNumber { get; set; }
        public string SubProgramName { get; set; }
        public string FormatName { get; set; }
        public string MealPlan { get; set; }
        public string Country { get; set; }
        public double TotalGrossPrice { get; set; }
        public double CommissionAddins { get; set; }
        public double Commision { get; set; }
        public double Paid { get; set; }
        public double Balance { get; set; }
        public string Email { get; set; }
        public double NetPrice { get; set; }

        public List<string> StudentPDFAddinInc { get; set; }
        public List<string> StudentPDFAddinAdd { get; set; }

}

   

}
