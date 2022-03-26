using MediatR;
using SPS.UI.Data.Models.ShoppingCart;
using System;
using System.Collections.Generic;
using System.Text;

namespace SPS.UI.Service.ShoppingCart.Command.AddShoppingCart
{
    public class AddShoppingCartRequest:IRequest<bool>
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
    }
}
