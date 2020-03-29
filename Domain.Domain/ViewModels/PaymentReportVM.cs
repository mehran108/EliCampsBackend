using System;
using System.Collections.Generic;
using System.Text;

namespace ELI.Domain.ViewModels
{
    public class PaymentReportVM
    {
        public int ID { get; set; }
        public int Year { get; set; }
        public string Reg_Ref { get; set; }
        public string CampusName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? AgencyID { get; set; }
        public int? GroupID { get; set; }
        public int? Campus { get; set; }
        public int? Format { get; set; }
        public double TotalGrossPrice { get; set; }
        public double TotalAddins { get; set; }
        public double Paid { get; set; }
        public double Commision { get; set; }
        public double CommissionAddins { get; set; }
        public double NetPrice { get; set; }
        public double Balance { get; set; }
        public string FormatName { get; set; }
        public string AgentName { get; set; }
        public string ProgramName { get; set; }
        public string SubProgramName { get; set; }
        public bool? Active { get; set; }
        public string ChapFamily { get; set; }
        public string AgencyRef { get; set; }
        public int? ProgramID { get; set; }
        public int? SubProgramID { get; set; }
    }
}
