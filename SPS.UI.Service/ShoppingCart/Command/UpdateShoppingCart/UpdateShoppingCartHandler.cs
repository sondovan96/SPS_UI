using MediatR;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using SPS.UI.Data.Core;
using SPS.UI.Data.Models.Product;
using SPS.UI.Data.Models.ShoppingCart;
using SPS.UI.Service.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SPS.UI.Service.ShoppingCart.Command.UpdateShoppingCart
{
    public class UpdateShoppingCartHandler : IRequestHandler<UpdateShoppingCartRequest, bool>
    {
        private readonly IHttpRequestExtension _httpRequestExtension;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UpdateShoppingCartHandler(IHttpRequestExtension httpRequestExtension, IHttpContextAccessor httpContextAccessor)
        {
            _httpRequestExtension = httpRequestExtension;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> Handle(UpdateShoppingCartRequest request, CancellationToken cancellationToken)
        {

            var session = _httpContextAccessor.HttpContext.Session.GetString(Constants.SessionKey.CartSession);
            List<ShoppingCartModel> currentShoppingCart = new List<ShoppingCartModel>();

            currentShoppingCart = JsonConvert.DeserializeObject<List<ShoppingCartModel>>(session);

            foreach (var item in currentShoppingCart)
            {
                var jsonItem = currentShoppingCart.SingleOrDefault(x => x.IdProduct == item.IdProduct);
                if (jsonItem != null)
                {
                    item.Quantity = request.Quantity;
                }
            }
            _httpContextAccessor.HttpContext.Session.SetString(Constants.SessionKey.CartSession, JsonConvert.SerializeObject(currentShoppingCart));
            return await Task.FromResult(true);
        }
    }
}
