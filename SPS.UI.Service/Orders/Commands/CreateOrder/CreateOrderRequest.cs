using MediatR;
using SPS.UI.Data.Core;
using SPS.UI.Data.Models.Order;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;


namespace SPS.UI.Service.Orders.Commands.CreateOrder
{
    public class CreateOrderRequest:IRequest<OrderModel>
    {

        public Guid UserId { get; set; }
        public string Recipient { get; set; }
        public string ShipAddress { get; set; }
        public string PhoneRecipient { get; set; }
        public decimal OrderTotal { get; set; }
        public string PaymentStatus { get; set; }
        public string SessionId { get; set; }
        public string PaymentIntentId { get; set; }
    }
}
