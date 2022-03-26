using MediatR;
using SPS.UI.Data.Core;
using SPS.UI.Data.Models.Order;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace SPS.UI.Service.Orders.Commands.UpdateOrderStatus
{
    public class UpdateOrderStatusRequest: IRequest<Response<OrderModel>>
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public string PaymentStatus { get; set; }
    }
}
