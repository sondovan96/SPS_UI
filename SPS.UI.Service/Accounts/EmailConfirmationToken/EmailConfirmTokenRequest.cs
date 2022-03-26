using MediatR;
using SPS.UI.Data.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace SPS.UI.Service.Accounts.EmailConfirmationToken
{
    public class EmailConfirmTokenRequest:IRequest<ViewInfo>
    {
        public string Email { get; set; }
        public string RedirectUrl { get; set; }
    }
}
