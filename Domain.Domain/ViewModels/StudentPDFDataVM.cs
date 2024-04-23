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
        public double TotalAddins { get; set; }
        public double Paid { get; set; }
        public double Balance { get; set; }
        public string Email { get; set; }
        public double NetPrice { get; set; }
        public double RegistrationFee { get; set; }
        public string Address { get; set; }
        public string EmailBody { get; set; }
        public string Subject { get; set; }
        public double? TotalPayment { get; set; }
        public string PassportNumber { get; set; }

        public List<string> StudentPDFAddinInc { get; set; }
        public List<string> StudentPDFAddinAdd { get; set; }
        public StudentPDFDataVM Clone()
        {
            StudentPDFDataVM newObj = new StudentPDFDataVM();
            newObj.FirstName = this.FirstName;
            newObj.LastName = this.LastName;
            newObj.AgentName = this.AgentName;
            newObj.AgentAddress = this.AgentAddress;
            newObj.AgentCountry = this.AgentCountry;
            newObj.Reg_Ref = this.Reg_Ref;
            newObj.CampusAddressOnReports = this.CampusAddressOnReports;
            newObj.DOB = this.DOB;
            newObj.ProgrameStartDate = this.ProgrameStartDate;
            newObj.ProgrameEndDate = this.ProgrameEndDate;
            newObj.ArrivalDate = this.ArrivalDate;
            newObj.ArrivalTime = this.ArrivalTime;
            newObj.ProgramName = this.ProgramName;
            newObj.FlightNumber = this.FlightNumber;
            newObj.SubProgramName = this.SubProgramName;
            newObj.FormatName = this.FormatName;
            newObj.MealPlan = this.MealPlan;
            newObj.Country = this.Country;
            newObj.TotalGrossPrice = this.TotalGrossPrice;
            newObj.CommissionAddins = this.CommissionAddins;
            newObj.Commision = this.Commision;
            newObj.TotalAddins = this.TotalAddins;
            newObj.Paid = this.Paid;
            newObj.Balance = this.Balance;
            newObj.Email = this.Email;
            newObj.NetPrice = this.NetPrice;
            newObj.RegistrationFee = this.RegistrationFee;
            newObj.Address = this.Address;
            newObj.EmailBody = this.EmailBody;
            newObj.Subject = this.Subject;
            newObj.TotalPayment = this.TotalPayment;
            newObj.PassportNumber = this.PassportNumber;
            newObj.StudentPDFAddinInc = this.StudentPDFAddinInc;
            newObj.StudentPDFAddinAdd = this.StudentPDFAddinAdd;
            return newObj;
        }

    }

}
