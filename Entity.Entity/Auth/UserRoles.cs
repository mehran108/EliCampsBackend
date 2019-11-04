using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ELI.Entity.Auth
{
    public partial class UserRoles
    {
        [Key]
        public int UserId { get; set; }
        public int RoleId { get; set; }

        public Roles Role { get; set; }
        public Users User { get; set; }
    }
}
