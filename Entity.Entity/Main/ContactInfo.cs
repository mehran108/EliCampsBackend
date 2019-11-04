using System;
using System.Collections.Generic;

namespace ELI.Entity.Main
{
    public partial class ContactInfo
    {
        public int Id { get; set; }
        public string AppName { get; set; }
        public string Address { get; set; }
        public string Floor { get; set; }
        public string Street { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}
