using MediatR;
using SPS.UI.Data.Core;
using SPS.UI.Data.Models.Account;
using System;
using System.Collections.Generic;
using System.Text;

namespace SPS.UI.Service.Accounts.LogIn
{
    public class LoginRequest:IRequest<ViewInfo>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string EmailConfirmRedirectUrl { get; set; }
        public bool Remember { get; set; }
    }
}
