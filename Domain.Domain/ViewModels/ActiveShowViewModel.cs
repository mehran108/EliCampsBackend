using System;
using System.Collections.Generic;
using System.Text;

namespace ELI.Domain.ViewModels
{
   public class ActiveShowViewModel
    {
        public string ActivationKey { get; set; }
      //  public int ShowId { get; set; }
        public string DeviceIdentifier { get; set; }
        public string StandNumber { get; set; }
        public string Company { get; set; }
        public string Name { get; set; }
        public string platform { get; set; }
        public string model { get; set; }
        public string manufacturer { get; set; }
        public string version { get; set; }

    }
}
