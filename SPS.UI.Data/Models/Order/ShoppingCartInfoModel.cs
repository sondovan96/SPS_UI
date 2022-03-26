using SPS.UI.Data.Models.ShoppingCart;
using System;
using System.Collections.Generic;
using System.Text;

namespace SPS.UI.Data.Models.Order
{
    public class ShoppingCartInfoModel
    {
        public List<ShoppingCartModel> ShoppingCartItems { get; set; }
        public OrderModel Order { get; set; }
    }
}
