using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MangaWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MangaWebApp.Controllers
{
    [Authorize(Roles = "Admin, User")]
    public class MangasController : Controller
    {
        private readonly MangaService _mangaService;
        private readonly CommentService _commentService;
        private readonly RatingService _ratingService;
        private readonly UserService _userService;

        public MangasController(MangaService mangaService, CommentService commentService, RatingService ratingService, UserService userService)
        {
            this._mangaService = mangaService;
            this._commentService = commentService;
            this._ratingService = ratingService;
            this._userService = userService;
        }

        public IActionResult Index()
        {
            var mangas = _mangaService.Get();

            foreach (var manga in mangas)
            {
                var sum = 0;
                var size = 0;
                var ratings = _mangaService.GetRatings(manga.Id);

                foreach (var rate in ratings)
                {
                    sum = sum + rate.Value;
                    size++;
                }

                if (size == 0)
                    manga.AverageRating = 0;
                else
                    manga.AverageRating = sum / size;
            }

            return View(new MangaView() { Mangas = mangas });
        }

        // GET: /<controller>/
        public IActionResult Details(int id)
        {
            // Get user ID
            var userEmail = HttpContext.User.FindFirst(ClaimTypes.Email).Value;
            User currUser = _userService.GetByEmail(userEmail);

            // Get comments
            var comments = _mangaService.GetComments(id);
            // Get user info
            foreach (var comment in comments)
            {
                int userId = comment.UserId;
                User user = _userService.Get(userId);

                comment.User = user;
            }

            var mark = _ratingService.GetRatingByUserIdAndMangaId(currUser.Id, id);

            // Get manga info
            var manga = _mangaService.Get(id);

            return View(new MangaView() { Manga = manga, Comments = comments, Mark = mark, CurrentUser = currUser});
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Manga manga)
        {
            if (ModelState.IsValid)
            {
                Manga tempManga = _mangaService.CreateManga(manga);

                if (tempManga != null)
                    return RedirectToAction("Index", "Mangas");

                ModelState.AddModelError(string.Empty, "Un problème est survenu lors de l'ajout du manga.");
                return View(manga);
            }

            return View(manga);
        }

        public IActionResult Delete(int id)
        {
            if (ModelState.IsValid)
            {
                _mangaService.DeleteManga(id);

                return RedirectToAction("Index", "Mangas");
            }

            return RedirectToAction("Index", "Mangas");
        }

        public IActionResult Modify(int id)
        {
            return View(_mangaService.Get(id));
        }

        [HttpPost]
        public IActionResult Modify(Manga manga)
        {
            if (ModelState.IsValid)
            {
                bool mangaModified = _mangaService.ModifyManga(manga);

                if (mangaModified)
                    return RedirectToAction("Index", "Mangas");

                ModelState.AddModelError(string.Empty, "Un problème est survenu lors de la modification du manga.");
                return View(manga);
            }

            return View(manga);
        }

        public IActionResult AddComment(Comment comment)
        {
            if (ModelState.IsValid)
            {
                Comment tempComment = _commentService.CreateComment(comment);
                
                return RedirectToAction("Details", "Mangas", new { id = tempComment.MangaId });
            }

            return RedirectToAction("Details", "Mangas", new { id = comment.MangaId });
        }

        public IActionResult AddRating(Rating rating)
        {
            if (ModelState.IsValid)
            {
                Rating tempRating = _ratingService.CreateRating(rating);

                return RedirectToAction("Details", "Mangas", new { id = tempRating.MangaId });
            }

            return RedirectToAction("Details", "Mangas", new { id = rating.MangaId });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
