using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SPS.UI.Service.Categories.Queries.GetAllCategory;
using System.Threading.Tasks;

namespace SPS.UI.Components
{
    [ViewComponent(Name = "TopMenu")]
    public class TopMenu : ViewComponent
    {
        private readonly IMediator _mediator;
        private readonly ILogger<TopMenu> _logger;

        public TopMenu(IMediator mediator, ILogger<TopMenu> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            GetAllCategoryRequest category = new GetAllCategoryRequest();
            var model = await _mediator.Send(category);
            return View(model.Source);
        }
    }
}
