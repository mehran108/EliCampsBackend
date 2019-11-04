using System;
using System.Collections.Generic;

namespace ELI.Entity.Main
{
    public partial class Region
    {
        public Region()
        {
            Pricing = new HashSet<Pricing>();
            Show = new HashSet<Show>();
        }

        public int RegionId { get; set; }
        public string RegionName { get; set; }
        public bool? IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool? IsDeleted { get; set; }
        public string CurrencyIso { get; set; }
        public string RegionUrl { get; set; }
        public string Flag { get; set; }
        public string VisitorsAPIURL { get; set; }
        public string UserManualURL { get; set; }
        public string PrivacyURL { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public Currency CurrencyIsoNavigation { get; set; }
        public ICollection<Pricing> Pricing { get; set; }
        public ICollection<Show> Show { get; set; }
       
    }
}
