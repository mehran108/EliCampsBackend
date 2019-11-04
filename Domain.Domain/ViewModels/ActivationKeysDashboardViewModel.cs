using System;
using System.Collections.Generic;
using System.Text;

namespace ELI.Domain.ViewModels
{
   public class ActivationKeysDashboardViewModel
    {
        public int ActivationId { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool? IsDeleted { get; set; }
        public int ActivationTypeId { get; set; }
        public string ActivationKey { get; set; }
        public int DeviceId { get; set; }

    }
}
