using MediatR;
using Microsoft.AspNetCore.Http;
using SPS.UI.Data.Core;
using SPS.UI.Data.Models.Account;
using SPS.UI.Service.Extensions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SPS.UI.Service.Accounts.EmailConfirmation
{
    public class EmailConfirmHandler : IRequestHandler<EmailConfirmRequest>
    {
        private readonly IHttpRequestExtension _httpRequestExtension;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public EmailConfirmHandler(IHttpRequestExtension httpRequestExtension, IHttpContextAccessor httpContextAccessor)
        {
            _httpRequestExtension = httpRequestExtension;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Unit> Handle(EmailConfirmRequest request, CancellationToken cancellationToken)
        {
            request.Token = request.Token.Replace(" ", "+");
            var response = await _httpRequestExtension.PostJsonRequestAsync<Response<AccountModel>>(Constants.ApiUrl.Account.ConfirmEmail, request, default);
            if (response.httpStatusCode.Equals(HttpStatusCode.OK))
            {
                _httpContextAccessor.HttpContext.Session
                    .SetString(Constants.SessionKey.Token, response.Data.Token.AccessToken);
                _httpContextAccessor.HttpContext.Session
                    .SetString(Constants.SessionKey.TokenScheme, response.Data.Token.TokenType);
                return Unit.Value;
            }
            throw new Exception();
        }
    }
}
