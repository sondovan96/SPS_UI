using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SPS.UI.Service.Products.Queries.GetNewProduct;
using System.Threading.Tasks;

namespace SPS.UI.Components
{
    [ViewComponent(Name = "NewProducts")]
    public class NewProducts : ViewComponent
    {
        private readonly IMediator _mediator;
        private readonly ILogger<NewProducts> _logger;

        public NewProducts(IMediator mediator, ILogger<NewProducts> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            GetNewProductRequest product = new GetNewProductRequest();
            var model = await _mediator.Send(product);
            return View(model.Source);
        }
    }
}
