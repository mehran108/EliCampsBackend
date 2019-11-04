using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ELI.Entity.Auth
{
    public partial class UserClaims
    {
        [Key]
        public int Id { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
        public int UserId { get; set; }

        public Users User { get; set; }
    }
}
