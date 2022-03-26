using MediatR;
using SPS.UI.Data.Core;
using SPS.UI.Data.Models.Order;
using SPS.UI.Service.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SPS.UI.Service.Orders.Queries.GetOrder
{
    public class GetOrderHandler : IRequestHandler<GetOrderRequest, OrderModel>
    {
        private readonly IHttpRequestExtension _httpRequestExtension;

        public GetOrderHandler(IHttpRequestExtension httpRequestExtension)
        {
            _httpRequestExtension = httpRequestExtension;
        }

        public async Task<OrderModel> Handle(GetOrderRequest request, CancellationToken cancellationToken)
        {
            var response = await _httpRequestExtension.GetRequestAsync<Response<OrderModel>>($"{Constants.ApiUrl.Order.Root}/{request.Id}");
            return response.Data;
        }
    }
}
