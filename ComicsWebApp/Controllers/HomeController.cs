﻿using ComicsWebApp.Data;
using ComicsWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace ComicsWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ComicsDbContext _context;

        public HomeController(ILogger<HomeController> logger, ComicsDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult AddComics()
        {
            var comicsViewModel = new ComicsViewModel();
            comicsViewModel.Genres = _context.ComicsGenres.Select(g => new SelectListItem { Text = g.GenreName, Value = g.Id.ToString() }).ToList();
            return View(comicsViewModel);
        }

        [HttpPost]
        public IActionResult AddComics(ComicsViewModel comicsViewModel, IFormFile CoverFile)
        {
            ComicsGenre comicsGenre = new ComicsGenre();
            comicsViewModel.ListOfGenres = new List<ComicsGenre>();

            Comics comics = new Comics();
            comics.Name = comicsViewModel.Comics.Name;
            comics.Author = comicsViewModel.Comics.Author;
            comics.Price = comicsViewModel.Comics.Price;
            comics.CoverType = comicsViewModel.Comics.CoverType;
            comics.Language = comicsViewModel.Comics.Language;
            comics.Publisher = comicsViewModel.Comics.Publisher;
            comics.AvailabilityStatus = comicsViewModel.Comics.AvailabilityStatus;
            comics.PagesNumber = comicsViewModel.Comics.PagesNumber;
            comics.PublicationFormat = comicsViewModel.Comics.PublicationFormat;
            comics.YearOfPublisihing = comicsViewModel.Comics.YearOfPublisihing;
            comics.Description = comicsViewModel.Comics.Description;

            if (comicsViewModel.GenresIds.Length > 0)
            {
                foreach (var genreid in comicsViewModel.GenresIds)
                {
                    comicsGenre = _context.ComicsGenres.FirstOrDefault(g => g.Id == genreid);
                    comics.Genres.Add(comicsGenre);
                    comicsViewModel.ListOfGenres.Add(comicsGenre);
                }
            }

            if (CoverFile.Length > 0)
            {
                using (var target = new MemoryStream())
                {
                    CoverFile.CopyTo(target);
                    comics.Cover = target.ToArray();
                }
            }

            _context.Comics.Add(comics);
            _context.SaveChanges();

            comicsViewModel.ComicsId = _context.Comics.FirstOrDefault(c => c.Name == comics.Name).Id;

            return View("ComicsInfo", comicsViewModel);
        }

        public ActionResult RenderPhoto(int id)
        {
            byte[] cover;
            var comics = _context.Comics.Find(id);
            if (!comics.Equals(null))
            {
                cover = comics.Cover;
                return File(cover, "image/jpg");
            }
            return View("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}