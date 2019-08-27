using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using MangaWebApp.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MangaWebApp.Controllers
{
    public class ProfileController : Controller
    {
        private readonly UserService _userService;
        //private readonly IHttpContextAccessor _httpContextAccessor;

        public ProfileController(UserService userService)
        {
            this._userService = userService;
            //_httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index()
        {
            var userEmail = HttpContext.User.FindFirst(ClaimTypes.Email).Value;

            //System.Diagnostics.Debug.WriteLine("=========================== " + identity);

            User user = _userService.GetByEmail(userEmail);

            return View(user);
        }

        [HttpPost]
        public IActionResult Index(User user)
        {
            ModelState.Remove("ConfirmPassword");

            if (ModelState.IsValid)
            {
                bool userModified = _userService.ModifyUser(user);

                if (userModified)
                {
                    HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                    //Create the identity for the user  
                    ClaimsIdentity identity = new ClaimsIdentity(new[] {
                        new Claim(ClaimTypes.Name, user.Name),
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim(ClaimTypes.Role, "Admin")
                    }, CookieAuthenticationDefaults.AuthenticationScheme);

                    var principal = new ClaimsPrincipal(identity);

                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    return RedirectToAction("Index", "Profile");
                }

                ModelState.AddModelError(string.Empty, "Un problème est survenu lors de la modification du compte.");
                return View(user);
            }

            return View(user);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}