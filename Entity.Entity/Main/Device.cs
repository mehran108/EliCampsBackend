using System;
using System.Collections.Generic;

namespace ELI.Entity.Main
{
    public partial class Device
    {
        public int DeviceId { get; set; }
        public string DeviceKey { get; set; }
        public string DeviceName { get; set; }
        public string DeviceDescription { get; set; }
        public bool? IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool? IsDeleted { get; set; }
        public string DeviceIdentifier { get; set; }
        public string platform { get; set; }
        public string model { get; set; }
        public string manufacturer { get; set; }
        public string version { get; set; }

    }
}
