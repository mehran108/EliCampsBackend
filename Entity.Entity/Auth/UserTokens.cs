using System;
using System.Collections.Generic;

namespace ELI.Entity.Auth
{
    public partial class UserTokens
    {
        public int UserId { get; set; }
        public string LoginProvider { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
