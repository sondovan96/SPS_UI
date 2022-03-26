using MediatR;
using SPS.UI.Data.Core;
using SPS.UI.Data.Models.Order;
using System;
using System.Collections.Generic;
using System.Text;

namespace SPS.UI.Service.Orders.Commands.CreateOrderDetail
{
    public class CreateOrderDetailRequest :IRequest<Response<OrderDetailModel>>
    {
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
