using BackendDeveloperProject.Core.Helpers;
using BackendDeveloperProject.Entities.Concrete;
using BackendDeveloperProject.Entities.DataTransferObjects.Request;
using BackendDeveloperProject.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackendDeveloperProject.UI.Controllers
{
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("[action]")]
        [Route("")]
        [AllowAnonymous]
        public IActionResult Login()
        {
            if (UserInfoExtensions.GetUserId() > 0)
                return RedirectToAction("Index", "Home");

            ViewData["Title"] = "Giriş Yap";
            ViewData["ReturnUrl"] = Request.Query["ReturnUrl"];
            return View();
        }

        [HttpPost]
        [Route("[action]")]
        [Route("")]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserLoginForm modelData, CancellationToken cancellationToken = default)
        {
            ViewData["Title"] = "Giriş Yap";
            ViewData["ReturnUrl"] = Request.Query["ReturnUrl"];

            Result? result = await _userService.Login(modelData, cancellationToken);
            if (result.IsSuccess)
            {
                if (Url.IsLocalUrl(modelData.ReturnUrl))
                    return LocalRedirect(modelData.ReturnUrl);
                else
                    return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("LoginValidate", result.Message);
                return View();
            }
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult Register()
        {
            ViewData["Title"] = "Kayıt Ol";
            return View();
        }

        [HttpGet]
        [Route("[action]")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            Result? result = await _userService.Logout();
            if (result.IsSuccess)
                return RedirectToAction("Index", "Home");
            else
            {
                ModelState.AddModelError("LoginValidate", result.Message);
                return Redirect(Request.Headers["Referer"].ToString());
            }
        }
    }
}
