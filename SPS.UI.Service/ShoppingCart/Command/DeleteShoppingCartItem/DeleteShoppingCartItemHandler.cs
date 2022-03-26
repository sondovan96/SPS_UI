using MediatR;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using SPS.UI.Data.Core;
using SPS.UI.Data.Models.ShoppingCart;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SPS.UI.Service.ShoppingCart.Command.DeleteShoppingCartItem
{
    public class DeleteShoppingCartItemHandler : IRequestHandler<DeleteShoppingCartItemRequest, List<ShoppingCartModel>>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DeleteShoppingCartItemHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<List<ShoppingCartModel>> Handle(DeleteShoppingCartItemRequest request, CancellationToken cancellationToken)
        {
            var sessionCart = _httpContextAccessor.HttpContext.Session.GetString(Constants.SessionKey.CartSession);
            List<ShoppingCartModel> currentShoppingCart = new List<ShoppingCartModel>();
            if(sessionCart !=null)
            {
                currentShoppingCart = JsonConvert.DeserializeObject<List<ShoppingCartModel>>(sessionCart);
            }
            currentShoppingCart.RemoveAll(x => x.IdProduct == request.Id);
            _httpContextAccessor.HttpContext.Session.SetString(Constants.SessionKey.CartSession, JsonConvert.SerializeObject(currentShoppingCart));
            return await Task.FromResult(currentShoppingCart);
        }
    }
}
