using MediatR;
using SPS.UI.Data.Core;
using SPS.UI.Data.Models.Order;
using SPS.UI.Service.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SPS.UI.Service.Orders.Commands.UpdateOrderStatus
{
    public class UpdateOrderStatusHandler : IRequestHandler<UpdateOrderStatusRequest, Response<OrderModel>>
    {
        private readonly IHttpRequestExtension _httpRequestExtension;

        public UpdateOrderStatusHandler(IHttpRequestExtension httpRequestExtension)
        {
            _httpRequestExtension = httpRequestExtension;
        }
        public async Task<Response<OrderModel>> Handle(UpdateOrderStatusRequest request, CancellationToken cancellationToken)
        {
            var result = await _httpRequestExtension.PutJsonRequestAsync<Response<OrderModel>>
                ($"{Constants.ApiUrl.Order.Root}/{request.Id}/status", request);
            return result;
        }
    }
}
