using System;
using System.Collections.Generic;
using System.Text;

namespace ELI.Domain.ViewModels
{
    public class LookupValueViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }
        public string Description { get; set; }
        public int LookupTableId { get; set; }
    }
}
