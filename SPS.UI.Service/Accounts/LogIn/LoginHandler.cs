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

namespace SPS.UI.Service.Accounts.LogIn
{
    public class LoginHandler : IRequestHandler<LoginRequest, ViewInfo>
    {
        private readonly IHttpRequestExtension _httpRequestExtension;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoginHandler(IHttpRequestExtension httpRequestExtension, IHttpContextAccessor httpContextAccessor)
        {
            _httpRequestExtension = httpRequestExtension;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ViewInfo> Handle(LoginRequest request, CancellationToken cancellationToken)
        {
            string viewName = "/Login";
            var response = await _httpRequestExtension.PostJsonRequestAsync<Response<AccountModel>>(Constants.ApiUrl.Account.Login, request, default);
            if (response.httpStatusCode.Equals(HttpStatusCode.OK))
            {
                if (response.Data.IsConfirmedEmail)
                {
                    viewName = "/Home/Index";
                    _httpContextAccessor.HttpContext.Session
                        .SetString(Constants.SessionKey.Token, response.Data.Token.AccessToken);
                    _httpContextAccessor.HttpContext.Session
                        .SetString(Constants.SessionKey.TokenScheme, response.Data.Token.TokenType);
                }
                else
                {
                    viewName = "~/Views/Account/EmailConfirm.cshtml";
                }
            }
            return new ViewInfo
            {
                RequestModel = request,
                ViewData = new Dictionary<string, IResponse<object>>
                {
                    {"Account",response }
                },
                ViewName = viewName,
            };
        }
    }
}
