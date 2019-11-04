using System;
using System.Collections.Generic;

namespace ELI.Entity.Auth
{
    public partial class Roles
    {
        public Roles()
        {
            AuthRoleClaims = new HashSet<RoleClaims>();
            AuthUserRoles = new HashSet<UserRoles>();
        }

        public int Id { get; set; }
        public string ConcurrencyStamp { get; set; }
        public string Name { get; set; }
        public string NormalizedName { get; set; }

        public ICollection<RoleClaims> AuthRoleClaims { get; set; }
        public ICollection<UserRoles> AuthUserRoles { get; set; }
    }
}
