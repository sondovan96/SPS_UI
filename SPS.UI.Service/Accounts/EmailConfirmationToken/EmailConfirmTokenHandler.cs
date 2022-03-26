using MediatR;
using SPS.UI.Data.Core;
using SPS.UI.Data.Models.Account;
using SPS.UI.Service.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SPS.UI.Service.Accounts.EmailConfirmationToken
{
    public class EmailConfirmTokenHandler:IRequestHandler<EmailConfirmTokenRequest,ViewInfo>
    {
        private readonly IHttpRequestExtension _httpRequestExtension;

        public EmailConfirmTokenHandler(IHttpRequestExtension httpRequestExtension)
        {
            _httpRequestExtension = httpRequestExtension;
        }

        public async Task<ViewInfo> Handle(EmailConfirmTokenRequest request, CancellationToken cancellationToken)
        {
            request.RedirectUrl = Constants.AppUrl.RedirectUrl;

            var response = await _httpRequestExtension.PostJsonRequestAsync<Response<object>>(Constants.ApiUrl.Account.SendConfirmEmail, request, default);
            if(response !=null)
            {
                return new ViewInfo
                {
                    RequestModel = request,
                    ViewName = "EmailConfirm"
                };
            }
            throw new Exception();
        }
    }
}
