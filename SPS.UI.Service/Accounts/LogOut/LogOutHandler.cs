using MediatR;
using Microsoft.AspNetCore.Http;
using SPS.UI.Data.Core;
using SPS.UI.Service.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SPS.UI.Service.Accounts.LogOut
{
    public class LogOutHandler : IRequestHandler<LogOutRequest, bool>
    {
        private readonly IHttpRequestExtension _httpRequestExtension;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LogOutHandler(IHttpRequestExtension httpRequestExtension, IHttpContextAccessor httpContextAccessor)
        {
            _httpRequestExtension = httpRequestExtension;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<bool> Handle(LogOutRequest request, CancellationToken cancellationToken)
        {
            _httpContextAccessor.HttpContext.Session.Clear();
            var response = await _httpRequestExtension.PostJsonRequestAsync<Response<object>>(Constants.ApiUrl.Account.Logout, default, default);
            return true;
        }
    }
}
