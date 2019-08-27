using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MangaWebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace MangaWebApp.Controllers
{
    public class SignupController : Controller
    {
        private readonly UserService _userService;

        public SignupController(UserService userService)
        {
            this._userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(User user)
        {
            if (ModelState.IsValid)
            {
                // TODO: CHECK IF EMAIL ALREADY EXISTS

                // ---------------

                User tempUser = _userService.CreateUser(user.Name, user.Email, user.Password);

                if (tempUser != null)
                    return RedirectToAction("Index", "Login", tempUser);

                ModelState.AddModelError(string.Empty, "Un problème est survenu lors de la création du compte.");
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