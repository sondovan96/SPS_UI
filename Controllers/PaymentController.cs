using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using SPS.UI.Data.Core;
using SPS.UI.Data.Models.Account;
using SPS.UI.Data.Models.Order;
using SPS.UI.Service.Accounts.Queries.GetUserCurrent;
using SPS.UI.Service.Extensions.EmailSender;
using SPS.UI.Service.Orders.Commands.CreateOrder;
using SPS.UI.Service.Orders.Commands.CreateOrderDetail;
using SPS.UI.Service.Orders.Commands.UpdateOrderStatus;
using SPS.UI.Service.Orders.Commands.UpdateStripePayment;
using SPS.UI.Service.Orders.Queries.GetOrder;
using SPS.UI.Service.ShoppingCart.Queries.GetShoppingCart;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SPS.UI.Controllers
{
    public class PaymentController : Controller
    {
        private readonly ILogger<PaymentController> _logger;
        private readonly IMediator _mediator;
        private readonly IGridEmailSender _emailSender;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PaymentController(ILogger<PaymentController> logger, IMediator mediator, IGridEmailSender emailSender, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _mediator = mediator;
            _emailSender = emailSender;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> Index()
        {
            GetShoppingCartRequest request = new GetShoppingCartRequest();
            ShoppingCartInfoModel cart = new ShoppingCartInfoModel()
            {
                ShoppingCartItems = await _mediator.Send(request),
                Order = new OrderModel()
            };
            foreach (var item in cart.ShoppingCartItems)
            {
                cart.Order.OrderTotal += item.Price;
            }

            return View(cart);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Checkout(string province, string district, string phonenumber, string recipient, string address)
        {

            var user = await _mediator.Send(new GetUserCurrentRequest());

            var item = await _mediator.Send(new GetShoppingCartRequest());

            OrderModel order = new OrderModel()
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                OrderDate = DateTime.UtcNow,
                OrderTotal = item.Sum(x => x.Price * x.Quantity),
                PhoneRecipient = phonenumber,
                Recipient = recipient,
                ShipAddress = $"{address},{district},{province}"
            };

            ShoppingCartInfoModel shoppingCart = new ShoppingCartInfoModel()
            {
                ShoppingCartItems = item,
                Order = order
            };
            CreateOrderRequest request = new CreateOrderRequest()
            {
                UserId = shoppingCart.Order.Id,
                OrderTotal = shoppingCart.Order.OrderTotal,
                PaymentStatus = "Pending",
                PhoneRecipient = shoppingCart.Order.PhoneRecipient,
                Recipient = shoppingCart.Order.Recipient,
                ShipAddress = shoppingCart.Order.ShipAddress,
            };

            var orderCreate = await _mediator.Send(request);

            var orderResult = orderCreate;

            foreach (var listItem in shoppingCart.ShoppingCartItems)
            {
                await _mediator.Send(new CreateOrderDetailRequest
                {
                    OrderId = orderResult.Id,
                    ProductId = listItem.IdProduct,
                    Price = listItem.Price,
                    Quantity = listItem.Quantity,
                });
            }

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string>
                {
                    "card",
                },
                LineItems = new List<SessionLineItemOptions>(),
                Mode = "payment",
                SuccessUrl = Url.ActionLink("PaymentConfirmation", "Payment", new { Id = orderResult.Id }),
                CancelUrl = Url.ActionLink("Index", "ShoppingCart"),
                CustomerEmail = user.Email,
            };

            foreach (var cartItem in shoppingCart.ShoppingCartItems)
            {
                var sessionLineItem = new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = (long)(cartItem.Price * 100),
                        Currency = "vnd",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = cartItem.ProductName,
                            Metadata = new Dictionary<string, string>()
                            {
                                {"Chủ shop đẹp trai","admin" }
                            }
                        },
                    },
                    Quantity = cartItem.Quantity,

                };
                options.LineItems.Add(sessionLineItem);
            }

            await _mediator.Send(new UpdateStripePaymentRequest
            {
                Id = orderResult.Id,
                SessionId = Guid.NewGuid().ToString(),
                PaymentIntentId = Guid.NewGuid().ToString(),
            });
            return Redirect(Url.ActionLink("PaymentConfirmation", "Payment", new { Id = orderResult.Id }));
        }

        public async Task<IActionResult> PaymentConfirmation([FromRoute] Guid Id)
        {
            var order = await _mediator.Send(new GetOrderRequest
            {
                Id = Id
            });

            await _mediator.Send(new UpdateOrderStatusRequest
            {
                Id = Id,
                PaymentStatus = "Approved"
            });

            var productList = _mediator.Send(new GetShoppingCartRequest()).Result.Select(x => x.IdProduct).ToList();
            var user = await _mediator.Send(new GetUserCurrentRequest());

            await _emailSender.SendEmailAsync
                (
                    user.Email,
                    "SP Shop Online - SPShop",
                    "<p>Order Created Successfully</p>"
                );
            _httpContextAccessor.HttpContext.Session.Clear();
            return View(Id);

        }
    }
}
