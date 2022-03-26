using MediatR;
using SPS.UI.Data.Core;
using SPS.UI.Data.Models.Order;
using SPS.UI.Service.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SPS.UI.Service.Orders.Commands.CreateOrderDetail
{
    public class CreateOrderDetailHandler : IRequestHandler<CreateOrderDetailRequest, Response<OrderDetailModel>>
    {
        private readonly IHttpRequestExtension _httpRequestExtension;

        public CreateOrderDetailHandler(IHttpRequestExtension httpRequestExtension)
        {
            _httpRequestExtension = httpRequestExtension;
        }
        public async Task<Response<OrderDetailModel>> Handle(CreateOrderDetailRequest request, CancellationToken cancellationToken)
        {
            var response = await _httpRequestExtension.PostJsonRequestAsync<Response<OrderDetailModel>>(Constants.ApiUrl.OrderDetail.Root, request);
            return response;
        }
    }
}
