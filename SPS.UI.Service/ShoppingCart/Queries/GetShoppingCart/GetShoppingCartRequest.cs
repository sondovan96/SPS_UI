using MediatR;
using SPS.UI.Data.Models.ShoppingCart;
using System;
using System.Collections.Generic;
using System.Text;

namespace SPS.UI.Service.ShoppingCart.Queries.GetShoppingCart
{
    public class GetShoppingCartRequest:IRequest<List<ShoppingCartModel>>
    {
    }
}
