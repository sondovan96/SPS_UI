using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SPS.UI.Service.Accounts.EmailConfirmation;
using SPS.UI.Service.Accounts.EmailConfirmationToken;
using SPS.UI.Service.Accounts.LogIn;
using SPS.UI.Service.Accounts.LogOut;
using SPS.UI.Service.Accounts.Registration;
using System.Threading.Tasks;

namespace SPS.UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IMediator _mediator;

        public AccountController(ILogger<AccountController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var viewInfo = await _mediator.Send(request);
            if (viewInfo.ViewName.Equals("/Home/Index"))
            {
                return Redirect(viewInfo.ViewName);
            }
            if (viewInfo.ViewName.Equals("~/Views/Account/EmailConfirm.cshtml"))
            {
                ViewData["Email"] = request.Email;
            }
            return View(viewInfo.ViewName, viewInfo);
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromForm]RegisterRequest request)
        {
            var viewInfo = await _mediator.Send(request);
            if (viewInfo.ViewName.Equals("/Home/Index"))
            {
                return View(viewInfo.ViewName);
            }
            if (viewInfo.ViewName.Equals("~/Views/Account/EmailConfirm.cshtml"))
            {
                ViewData["Email"] = request.Email;
            }
            return View(viewInfo.ViewName, viewInfo);
        }

        public async Task<IActionResult> Logout()
        {
            await _mediator.Send(new LogOutRequest());
            return View("/Home/Index");
        }
        public async Task<IActionResult> EmailConfirm([FromQuery] EmailConfirmRequest request)
        {
            await _mediator.Send(request);
            return Redirect("/Home/Index");
        }

        [HttpPost]
        public async Task<IActionResult> SendConfirmEmail([FromForm] EmailConfirmTokenRequest request)
        {
            var viewInfo = await _mediator.Send(request);
            ViewData["Email"] = request.Email;
            return View(viewInfo.ViewName, viewInfo);
        }
    }
}
