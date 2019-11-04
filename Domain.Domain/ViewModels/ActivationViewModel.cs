using ELI.Entity.Main;
using System;
using System.Collections.Generic;
using System.Text;

namespace ELI.Domain.ViewModels
{
    public class ActivationViewModel
    {
        public int? ActivationId { get; set; }
        public int? ActivationTypeId { get; set; }
        public string ActivationName { get; set; }
        public bool IsActive { get; set; }
        public ICollection<ActivationType> ActivationType { get; set; } = new HashSet<ActivationType>();
    }
}
