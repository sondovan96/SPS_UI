using MediatR;
using Microsoft.AspNetCore.Http;
using SPS.UI.Data.Core;
using SPS.UI.Data.Models.Account;
using SPS.UI.Data.Models.Role;
using SPS.UI.Service.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SPS.UI.Service.Accounts.Registration
{
    public class RegisterHandler : IRequestHandler<RegisterRequest, ViewInfo>
    {
        private readonly IHttpRequestExtension _httpRequestExtension;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RegisterHandler(IHttpRequestExtension httpRequestExtension, IHttpContextAccessor httpContextAccessor)
        {
            _httpRequestExtension = httpRequestExtension;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ViewInfo> Handle(RegisterRequest request, CancellationToken cancellationToken)
        {
            string viewName = "/Account/Index";
            var roles = _httpRequestExtension
                    .GetRequestAsync<Response<List<RoleModel>>>(Constants.ApiUrl.Role.GetRoles, default);
            
            request.RoleId = roles.Result.Data.FirstOrDefault(x => x.Name.Equals("Customer")).Id;
            
            var response = await _httpRequestExtension
                .PostJsonRequestAsync<Response<AccountModel>>(Constants.ApiUrl.Account.Register, request, default);
            Response < List < RoleModel >> roleResponse = null;
            if (response != null && response.httpStatusCode.Equals(HttpStatusCode.Created))
            {
                if (response.Data.IsConfirmedEmail)
                {
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
            else
            {
                Task<Response<List<RoleModel>>> taskRole = _httpRequestExtension
                    .GetRequestAsync<Response<List<RoleModel>>>(Constants.ApiUrl.Role.GetRoles, default);
                roleResponse = await taskRole;
            }
            return new ViewInfo
            {
                ViewData = new Dictionary<string, IResponse<object>>
                {
                    {"Account", response },
                    {"Role", roleResponse }
                },
                ViewName = viewName,
                RequestModel = request
            };

        }
    }
}
