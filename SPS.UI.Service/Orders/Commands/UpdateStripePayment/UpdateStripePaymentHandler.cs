using MediatR;
using SPS.UI.Data.Core;
using SPS.UI.Data.Models.Order;
using SPS.UI.Service.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SPS.UI.Service.Orders.Commands.UpdateStripePayment
{
    public class UpdateStripePaymentHandler : IRequestHandler<UpdateStripePaymentRequest, Response<OrderModel>>
    {
        private readonly IHttpRequestExtension _httpRequestExtension;

        public UpdateStripePaymentHandler(IHttpRequestExtension httpRequestExtension)
        {
            _httpRequestExtension = httpRequestExtension;
        }
        public async Task<Response<OrderModel>> Handle(UpdateStripePaymentRequest request, CancellationToken cancellationToken)
        {
            var result = await _httpRequestExtension.PutJsonRequestAsync<Response<OrderModel>>
                ($"{Constants.ApiUrl.Order.Root}/{request.Id}/stripe", request, default);
            return result;
        }
    }
}
