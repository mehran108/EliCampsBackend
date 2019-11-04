using System;
using System.Collections.Generic;
using System.Text;

namespace ELI.Domain.ViewModels
{
    public class EmailTemplateViewModel
    {
        public string Body { get; set; }
        public string Subject { get; set; }
        public string RegionName { get; set; }
        public string RegionId { get; set; }

        public string TemplateType { get; set; }
    }
}
