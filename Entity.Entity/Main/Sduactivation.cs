using ELI.Entity.Auth;
using System;
using System.Collections.Generic;

namespace ELI.Entity.Main
{
    public partial class Sduactivation
    {
        public int SduactivationId { get; set; }
        public int ShowId { get; set; }
        public int UserId { get; set; }
        public int ActivationId { get; set; }
        public int? DeviceId { get; set; }
        public DateTime? ActivationTime { get; set; }
        public string Company { get; set; }
        public string StandNumber { get; set; }
        public string Name { get; set; }
        public bool? IsConsumed { get; set; }
        public bool? IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool? IsDeleted { get; set; }
        public Users User { get; set; }

        public Activation Activation { get; set; }
    }
}
