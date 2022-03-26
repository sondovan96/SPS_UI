using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SPS.UI.Data.Models.Order;
using SPS.UI.Service.ShoppingCart.Command.AddShoppingCart;
using SPS.UI.Service.ShoppingCart.Command.DeleteShoppingCartItem;
using SPS.UI.Service.ShoppingCart.Command.UpdateShoppingCart;
using SPS.UI.Service.ShoppingCart.Queries.GetShoppingCart;
using System;
using System.Threading.Tasks;

namespace SPS.UI.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly ILogger<ShoppingCartController> _logger;
        private readonly IMediator _mediator;


        public ShoppingCartController(ILogger<ShoppingCartController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            GetShoppingCartRequest request = new GetShoppingCartRequest();
            ShoppingCartInfoModel cart = new ShoppingCartInfoModel()
            {
                ShoppingCartItems = await _mediator.Send(request),
                Order = new OrderModel()
            };
            foreach(var item in cart.ShoppingCartItems)
            {
                cart.Order.OrderTotal += item.Price;
            }

            return View(cart);
        }

        public async Task<JsonResult> AddShoppingCart(AddShoppingCartRequest request,int detailquantity)
        {
            //AddShoppingCartRequest request = new AddShoppingCartRequest();

            if (detailquantity != 1 && request.Quantity == 0)
            {
                request.Quantity = detailquantity;
            }
            else
            {
                request.Quantity = 1;
            }
            if (await _mediator.Send(request) != false)
            {
                return Json(new
                {
                    status = true
                });
            }
            return Json(new
            {
                status = false
            });
        }

        public async Task<JsonResult> UpdateShoppingCart(UpdateShoppingCartRequest request)
        {
            //UpdateShoppingCartRequest request = new UpdateShoppingCartRequest();
            //request.Quantity = Quantity;
            if (await _mediator.Send(request) != false)
            {
                return Json(new
                {
                    status = true
                });
            }
            return Json(new
            {
                status = false
            });
        }

        public async Task<JsonResult> DeleteShoppingCart(Guid Id)
        {
            DeleteShoppingCartItemRequest request = new DeleteShoppingCartItemRequest();
            request.Id = Id;
            try
            {
                await _mediator.Send(request);
                return Json(new
                {
                    status = true
                });
            }
            catch (Exception)
            {
                return Json(new
                {
                    status = false
                });
            }
        }
    }
}
