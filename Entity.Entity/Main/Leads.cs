using System;
using System.Collections.Generic;

namespace ELI.Entity.Main
{
    public partial class Leads
    {
        public Leads()
        {
            LeadsQualifier = new HashSet<LeadsQualifier>();
        }

        public int LeadsId { get; set; }
        public int? Sduid { get; set; }
        public string FirstName { get; set; }
        public string SurName { get; set; }
        public string Company { get; set; }
        public string Phone { get; set; }
        public string Designation { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public string CountryCode { get; set; }
        public string Barcode { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsActive { get; set; }
        public string Email { get; set; }
        public string Address2 { get; set; }
        public string State { get; set; }
        public string Suburb { get; set; }
        public string Landline { get; set; }
        public ICollection<LeadsQualifier> LeadsQualifier { get; set; }
    }
}
