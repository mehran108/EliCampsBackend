using ELI.Entity.Auth;
using ELI.Entity.Main;
using System;
using System.Collections.Generic;
using System.Text;

namespace ELI.Domain.ViewModels
{
   public class AccountListReportViewModel
    {
        public int InvoiceId { get; set; }
        public string FirstName { get; set; }
        public string SurName { get; set; }
        public string Company { get; set; }
        public string StreetAddress { get; set; }
        public string Suburb { get; set; }
        public string State_Province { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime? Purchased { get; set; }
        public decimal? Total { get; set; }
        public int? Quantity { get; set; }
        public int UsedCodes { get; set; }
        public int UnusedCodes { get; set; }
        public int TotalLeads { get; set; }
        public int TotalCodes { get; set; }
        public string Show { get; set; }


        //public int? ShowId { get; set; }
        //public int? UserId { get; set; }
        //public bool? IsActive { get; set; }
        //public string CreatedBy { get; set; }
        //public string UpdatedBy { get; set; }
        //public DateTime? UpdatedDate { get; set; }
        //public bool? IsDeleted { get; set; }
        //public Show Show { get; set; }
        //public bool IsSuccess { get; set; }
        // public Users user { get; set; }
    }
}
