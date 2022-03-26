using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace SPS.UI.Service.ShoppingCart.Command.UpdateShoppingCart
{
    public class UpdateShoppingCartRequest : IRequest<bool>
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
    }
}
