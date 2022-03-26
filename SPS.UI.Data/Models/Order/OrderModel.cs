using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace SPS.UI.Data.Models.Order
{
    public class OrderModel
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Recipient { get; set; }
        public string ShipAddress { get; set; }
        public string PhoneRecipient { get; set; }
        public string OrderStatus { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal OrderTotal { get; set; }
        public string PaymentStatus { get; set; }
        public string SessionId { get; set; }
        public string PaymentIntentId { get; set; }
    }
}
