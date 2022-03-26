using MediatR;
using SPS.UI.Data.Models.ShoppingCart;
using System;
using System.Collections.Generic;
using System.Text;

namespace SPS.UI.Service.ShoppingCart.Command.DeleteShoppingCartItem
{
    public class DeleteShoppingCartItemRequest:IRequest<List<ShoppingCartModel>>
    {
        public Guid Id { get; set; }
    }
}
