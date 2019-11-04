using System;
using System.Collections.Generic;
using System.Text;

namespace ELI.Domain.ViewModels
{
    public class AgentViewModel
    {
        public int ID { get; set; }
        public string Agent { get; set; }
        public string Contact { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Web { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public string Notes { get; set; }
        public string Other { get; set; }

    }
}
