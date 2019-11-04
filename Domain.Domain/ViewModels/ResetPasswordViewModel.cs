using System;
using System.Collections.Generic;
using System.Text;

namespace ELI.Domain.ViewModels
{
   public class ResetPasswordViewModel
    {
      
        public string SecurityStamp { get; set; }
        public string Email { get; set; }
        public string NewPassword { get; set; }
    }
}
