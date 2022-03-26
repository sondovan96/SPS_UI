using System;
using System.Collections.Generic;
using System.Text;

namespace SPS.UI.Data.Models.Order
{
    public class OrderDetailModel
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
