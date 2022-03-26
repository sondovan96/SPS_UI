using MediatR;
using Microsoft.AspNetCore.Http;
using SPS.UI.Data.Core;
using SPS.UI.Data.Models;
using SPS.UI.Data.Models.Account;
using SPS.UI.Service.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SPS.UI.Service.Accounts.Queries.GetUserCurrent
{
    public class GetUserCurrentHandler : IRequestHandler<GetUserCurrentRequest, AccountDetailModel>
    {
        private readonly IHttpRequestExtension _httpRequestExtension;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetUserCurrentHandler(IHttpRequestExtension httpRequestExtension, IHttpContextAccessor httpContextAccessor)
        {
            _httpRequestExtension = httpRequestExtension;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<AccountDetailModel> Handle(GetUserCurrentRequest request, CancellationToken cancellationToken)
        {
            Response<AccountDetailModel> response = null;
            var token = _httpContextAccessor.HttpContext.Session.GetString(Constants.SessionKey.Token);

            response = await _httpRequestExtension.GetRequestAsync<Response<AccountDetailModel>>
                (Constants.ApiUrl.Account.Root, new List<QueryPamaramsModel>
                {
                    new QueryPamaramsModel() { Key = "Bearer", Value = token }
                });
            return response.Data;
        }
    }
}
