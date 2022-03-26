using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SPS.UI.Service.Extensions.EmailSender
{
    public interface IGridEmailSender
    {
        Task SendEmailAsync(string email, string subject, string htmlMessage);
    }
}
