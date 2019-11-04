using System;
using System.Collections.Generic;

namespace ELI.Entity.Main
{
    public partial class ActivationType
    {
        public ActivationType()
        {
            Activation = new HashSet<Activation>();
        }

        public int ActivationTypeId { get; set; }
        public string ActivationTypeName { get; set; }
        public bool? IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool? IsDeleted { get; set; }

        public ICollection<Activation> Activation { get; set; }
    }
}
