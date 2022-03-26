using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SPS.UI.Service.Products.Queries.GetFeaturedProduct;
using System.Threading.Tasks;

namespace SPS.UI.Components
{
    [ViewComponent(Name = "FeaturedProducts")]
    public class FeaturedProducts : ViewComponent
    {
        private readonly IMediator _mediator;
        private readonly ILogger<FeaturedProducts> _logger;

        public FeaturedProducts(IMediator mediator, ILogger<FeaturedProducts> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            GetFeaturedProductRequest product = new GetFeaturedProductRequest();
            var model = await _mediator.Send(product);
            return View(model.Source);
        }
    }
}
