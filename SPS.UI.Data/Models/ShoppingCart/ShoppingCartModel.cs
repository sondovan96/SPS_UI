using System;
using System.Collections.Generic;
using System.Text;

namespace SPS.UI.Data.Models.ShoppingCart
{
    public class ShoppingCartModel
    {
        public Guid IdProduct { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
