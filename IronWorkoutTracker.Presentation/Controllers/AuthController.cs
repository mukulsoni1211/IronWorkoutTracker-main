using System.Security.Claims;
using IronWorkoutTracker.Application.IServices;
using IronWorkoutTracker.Presentation.PresentationConstants;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace IronWorkoutTracker.Presentation.Controllers
{
    public class AuthController : Controller
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }
         
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(string email , string password)
        {
            var user = await _userService.GetUserByEmailAndPassword(email, password);

            if(user == null)
            {
                ViewBag.Error = "Invalid Username or Password";
                return View();
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimConstants.Id, user.UserId.ToString()),
                new Claim(ClaimConstants.Name, user.UserName),
                new Claim(ClaimConstants.Email, user.Email),
                new Claim(ClaimConstants.Role, user.Role),
                
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            return RedirectToAction("Index", "Home");
             
        }


        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }


        public async Task<IActionResult> AccessDenied()
        {
            return Ok();
        }
    }
}
