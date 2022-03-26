using MediatR;
using SPS.UI.Data.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace SPS.UI.Service.Accounts.Registration
{
    public class RegisterRequest:IRequest<ViewInfo>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Guid RoleId { get; set; }
        public string About { get; set; }
        public string EmailConfirmationRedirectUrl { get; set; } = "/Account/EmailConfirm";
    }
}
