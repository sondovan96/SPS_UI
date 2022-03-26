using MediatR;
using SPS.UI.Data.Models.Order;
using System;
using System.Collections.Generic;
using System.Text;

namespace SPS.UI.Service.Orders.Queries.GetOrder
{
    public class GetOrderRequest:IRequest<OrderModel>
    {
        public Guid Id { get; set; }
    }
}
