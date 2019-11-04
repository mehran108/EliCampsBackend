using System;
using System.Collections.Generic;

namespace ELI.Entity.Main
{
    public partial class Activation
    {
        public Activation()
        {
            Sduactivation = new HashSet<Sduactivation>();
        }

        public int ActivationId { get; set; }
        public bool? IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool? IsDeleted { get; set; }
        public int ActivationTypeId { get; set; }
        public string ActivationKey { get; set; }
        public bool? IsConsumed { get; set; }
        public int? ShowId { get; set; }
        public int? InvoiceId { get; set; }

        public Show show { get; set; }
        public Invoice Invoice { get; set; }

        public ICollection<Sduactivation> Sduactivation { get; set; }
        public ActivationType ActivationType { get; set; }

    }
}
