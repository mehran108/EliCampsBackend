using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ELI.Entity.Auth
{
    public partial class RoleClaims
    {
        [Key]
        public int Id { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
        public int RoleId { get; set; }

        public Roles Role { get; set; }
    }
}
