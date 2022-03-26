using System;
using System.Collections.Generic;
using System.Text;

namespace SPS.UI.Data.Models.Account
{
    public class ChangePasswordModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string NewPassword { get; set; }
    }
}
