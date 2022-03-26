using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SPS.UI.Service.Products.Queries.GetHotProduct;
using System.Threading.Tasks;

namespace SPS.UI.Components
{
    [ViewComponent(Name = "HotProducts")]
    public class HotProducts : ViewComponent
    {
        private readonly IMediator _mediator;
        private readonly ILogger<HotProducts> _logger;

        public HotProducts(IMediator mediator, ILogger<HotProducts> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            GetHotProductRequest product = new GetHotProductRequest();
            var model = await _mediator.Send(product);
            return View(model.Source);
        }
    }
}

