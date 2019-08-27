using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using MangaWebApp.Models;
using System.Diagnostics;

namespace MangaWebApp.Controllers
{
    public class LoginController : Controller
    {
        private readonly UserService _userService;

        public LoginController(UserService userService)
        {
            this._userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(User user)
        {
            ModelState.Remove("Email");
            ModelState.Remove("ConfirmPassword");

            if (ModelState.IsValid)
            {
                ClaimsIdentity identity = null;
                bool isAuthenticated = false;

                User tempUser = _userService.GetByUsernameAndPassword(user.Name, user.Password);

                if (tempUser == null)
                {
                    ModelState.AddModelError(string.Empty, "Nom d'utilisateur ou mot de passe incorrect.");

                    return View(user);
                }
                else
                {
                    //Create the identity for the user  
                    identity = new ClaimsIdentity(new[] {
                        new Claim(ClaimTypes.Name, tempUser.Name),
                        new Claim(ClaimTypes.Email, tempUser.Email),
                        new Claim(ClaimTypes.Role, "Admin")
                    }, CookieAuthenticationDefaults.AuthenticationScheme);

                    isAuthenticated = true;
                }

                if (isAuthenticated)
                {
                    var principal = new ClaimsPrincipal(identity);

                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    return RedirectToAction("Index", "Home");
                }

                return RedirectToAction("Index");
            }

            return View(user);
        }

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}