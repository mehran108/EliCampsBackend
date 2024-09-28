using System;
using System.Collections.Generic;
using System.Text;

namespace ELI.Domain.ViewModels
{
    public class StudentsAgainstGroup
    {
        public double TotalGrossPrice { get; set; }
        public double Paid { get; set; }
        public double NetPrice { get; set; }
        public double Commision { get; set; }
        public double TotalAddins { get; set; }
        public double CommissionAddins { get; set; }
        public int TotalStudents { get; set; }
    }
}
