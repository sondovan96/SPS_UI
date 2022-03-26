using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SPS.UI.Service.Products.Queries.GetProductDetail;
using System;
using System.Threading.Tasks;

namespace SPS.UI.Controllers
{
   
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IMediator _mediator;

        public ProductController(ILogger<ProductController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet("Detail/{Id}")]
        public async Task<IActionResult> ProductDetail([FromRoute] GetProductDetailRequest request)
        {
            var model = await _mediator.Send(request);
            return View(model);
        }

    }
}
