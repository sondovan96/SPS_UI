using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace SPS.UI.Service.Accounts.EmailConfirmation
{
    public class EmailConfirmRequest:IRequest
    {
        [FromQuery]
        public string Email { get; set; }
        [FromQuery]
        public string Token { get; set; }
    }
}
