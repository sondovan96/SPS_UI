using MediatR;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using SPS.UI.Data.Core;
using SPS.UI.Data.Models.Product;
using SPS.UI.Data.Models.ShoppingCart;
using SPS.UI.Service.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SPS.UI.Service.ShoppingCart.Command.AddShoppingCart
{
    public class AddShoppingCartHandler : IRequestHandler<AddShoppingCartRequest, bool>
    {
        private readonly IHttpRequestExtension _httpRequestExtension;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AddShoppingCartHandler(IHttpRequestExtension httpRequestExtension, IHttpContextAccessor httpContextAccessor)
        {
            _httpRequestExtension = httpRequestExtension;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> Handle(AddShoppingCartRequest request, CancellationToken cancellationToken)
        {
            var product = await _httpRequestExtension.GetRequestAsync<Response<ProductModel>>($"{Constants.ApiUrl.Product.GetProduct}/{request.Id}");

            var session = _httpContextAccessor.HttpContext.Session.GetString(Constants.SessionKey.CartSession);
            List<ShoppingCartModel> currentShoppingCart = new List<ShoppingCartModel>();
            if (session != null)
            {
                currentShoppingCart = JsonConvert.DeserializeObject<List<ShoppingCartModel>>(session);
            }

            if (currentShoppingCart.Exists(x => x.IdProduct == request.Id))
            {
                foreach (var item in currentShoppingCart)
                {
                    if (item.IdProduct == request.Id)
                    {
                        item.Quantity += request.Quantity;
                    }
                }
            }
            else
            {
                var shoppingCart = new ShoppingCartModel()
                {
                    IdProduct = product.Data.Id,
                    Image = product.Data.Image,
                    Price = product.Data.Price,
                    ProductName = product.Data.ProductName,
                    Quantity = request.Quantity,
                };
                currentShoppingCart.Add(shoppingCart);

            }



            _httpContextAccessor.HttpContext.Session.SetString(Constants.SessionKey.CartSession, JsonConvert.SerializeObject(currentShoppingCart));
            return true;
        }
    }
}
