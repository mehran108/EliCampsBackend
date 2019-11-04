using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ELI.Entity.Auth
{
    public partial class UserLogins
    {
        
        public string LoginProvider { get; set; }
        public string ProviderKey { get; set; }
        public string ProviderDisplayName { get; set; }
        [Key]
        public int UserId { get; set; }
        public Users User { get; set; }
    }
}
