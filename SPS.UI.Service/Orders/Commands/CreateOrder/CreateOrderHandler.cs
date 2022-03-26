using MediatR;
using SPS.UI.Data.Core;
using SPS.UI.Data.Models.Order;
using SPS.UI.Service.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SPS.UI.Service.Orders.Commands.CreateOrder
{
    public class CreateOrderHandler : IRequestHandler<CreateOrderRequest, OrderModel>
    {
        private readonly IHttpRequestExtension _httpRequestExtension;

        public CreateOrderHandler(IHttpRequestExtension httpRequestExtension)
        {
            _httpRequestExtension = httpRequestExtension;
        }

        public async Task<OrderModel> Handle(CreateOrderRequest request, CancellationToken cancellationToken)
        {

            var response = await _httpRequestExtension.PostJsonRequestAsync<Response<OrderModel>>(Constants.ApiUrl.Order.AddOrder, request, default);
            return response.Data;
        }
    }
}
