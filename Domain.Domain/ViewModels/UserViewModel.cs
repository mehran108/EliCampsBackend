using ELI.Entity.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ELI.Domain.ViewModels
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string SurName { get; set; }
        public string StreetAddress { get; set; }
        public string Company { get; set; }
        public string Suburb { get; set; }
        public string State_Province { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public int RoleId { get; set; }
    }
}
