using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using PortailEbook.Extensions;
using PortailEbook.Models;
using PortailEbook.Models.BLL;
using PortailEbook.Models.Entity;
using System.Security.Claims;

namespace PortailEbook.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            if(User.FindFirst(claim => claim.Type == System.Security.Claims.ClaimTypes.Name)?.Value != null)
			{
                return RedirectToAction("Index", "Role");
			}
            return View();
        }

        // check if the user Exists and validate Email and Password
        [HttpPost]
        public JsonResponse Login(Login loginUser)
        {
            User user = BLLUser.getUserBy("Email", loginUser.Email);
            JsonResponse jsonResponse = new JsonResponse();
            if (user == null)
            {
                jsonResponse.message = "User Not Found !";
                jsonResponse.success = false;
                return jsonResponse;
            }
            else if (user.Password != loginUser.Password)
            {
                jsonResponse.message = "Password Incorrect";
                jsonResponse.success = false;
                return jsonResponse;
            }
            else
            {
                jsonResponse.message = "Login succesful";
                jsonResponse.success = true;
                return jsonResponse;
            }
        }

        // Authenticate the User and save his details
        [HttpPost]
        public JsonResponse LoginUser(Login loginUser)
        {
            JsonResponse jsonResponse = new JsonResponse();
            User user = BLLUser.getUserBy("Email", loginUser.Email);
            var identity = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, loginUser.Email ),
                    new Claim(ClaimTypes.Role, user.Profil)
                }, CookieAuthenticationDefaults.AuthenticationScheme);

            var principal = new ClaimsPrincipal(identity);

            var login = HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity));
            jsonResponse.success = true;
            jsonResponse.message = "Login Successfully";
            return jsonResponse;
        }

        // Logout from account
        [HttpGet]
        public IActionResult Logout()
        {
            var login = HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        // Redirect to a view access denied if the user not allowed
        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

        // Reset password View
        [HttpGet]
        public IActionResult ResetPassword()
        {
            return View();
        }

        // Return if the user with email Exists
        public JsonResponse CheckUser(string email)
        {
            JsonResponse jsonResponse = new JsonResponse();
            User user = BLLUser.getUserBy("Email", email);
            if (user != null)
            {
                jsonResponse.success = true;
                jsonResponse.message = "Exist";
            }
            else
            {
                jsonResponse.success = false;
                jsonResponse.message = "Not Exist";
            }
            return jsonResponse;
        }

        // Reset Password
        [HttpPost]
        public IActionResult ResetPassword(string email)
        {
            JsonResponse jsonResponse = CheckUser(email);
            if (jsonResponse.success == true)
            {
                User user = BLLUser.getUserBy("Email", email);
                EmailManager.SendEmail(user);
            }
            return View();
        }
    }
}
