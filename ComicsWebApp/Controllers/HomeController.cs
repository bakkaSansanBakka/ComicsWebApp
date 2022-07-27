using AutoMapper;
using ComicsWebApp.Data;
using ComicsWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace ComicsWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly string imageFileType = "image/jpg";
        private readonly ILogger<HomeController> _logger;
        private readonly ComicsDbContext _context;
        private readonly IMapper _mapper;

        public HomeController(ILogger<HomeController> logger, ComicsDbContext context, IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
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
            comicsViewModel.AllGenresList = _context.ComicsGenres.Select(g => new SelectListItem { Text = g.GenreName, Value = g.Id.ToString() }).ToList();
            return View(comicsViewModel);
        }

        [HttpPost]
        public IActionResult AddComics(ComicsViewModel comicsViewModel, IFormFile coverFile)
        {
            comicsViewModel.ListOfGenres = new List<ComicsGenre>();
            comicsViewModel.ListOfPages = new List<ComicsPages>();

            Comics comics = new Comics
            {
                Name = comicsViewModel.Comics.Name,
                Author = comicsViewModel.Comics.Author,
                Price = comicsViewModel.Comics.Price,
                CoverType = comicsViewModel.Comics.CoverType,
                Language = comicsViewModel.Comics.Language,
                Publisher = comicsViewModel.Comics.Publisher,
                AvailabilityStatus = comicsViewModel.Comics.AvailabilityStatus,
                PagesNumber = comicsViewModel.Comics.PagesNumber,
                PublicationFormat = comicsViewModel.Comics.PublicationFormat,
                YearOfPublisihing = comicsViewModel.Comics.YearOfPublisihing,
                Description = comicsViewModel.Comics.Description,
            };

            if (comicsViewModel.GenresIds.Length > 0)
            {
                foreach (var genreid in comicsViewModel.GenresIds)
                {
                    var comicsGenre = _context.ComicsGenres.FirstOrDefault(g => g.Id == genreid);
                    comics.Genres.Add(comicsGenre);
                    comicsViewModel.ListOfGenres.Add(comicsGenre);
                }
            }

            if (coverFile.Length > 0)
            {
                using (var target = new MemoryStream())
                {
                    coverFile.CopyTo(target);
                    comics.Cover = target.ToArray();
                }
            }

            _context.Comics.Add(comics);
            _context.SaveChanges();

            comicsViewModel.Comics.Id = _context.Comics.FirstOrDefault(c => c.Name == comics.Name).Id;

            var comicsResponseViewModel = _mapper.Map<ComicsViewModel>(comicsViewModel);

            return View("ComicsInfo", comicsResponseViewModel);
        }

        public ActionResult RenderPhoto(int id)
        {
            byte[] cover;
            var comics = _context.Comics.Find(id);
            if (!comics.Equals(null))
            {
                cover = comics.Cover;
                return File(cover, imageFileType);
            }
            return View("Index");
        }

        public ActionResult RenderPage(int id)
        {
            byte[] page;
            var comicsPage = _context.ComicsPages.Find(id);
            if (!comicsPage.Equals(null))
            {
                page = comicsPage.Content;
                return File(page, imageFileType);
            }
            return View("Index");
        }

        [HttpPost]
        public async Task<IActionResult> AddPages(List<IFormFile> pagesFiles, int id)
        {
            var comics = _context.Comics.Include(c => c.Genres).FirstOrDefault(c => c.Id == id);

            foreach (var pageFile in pagesFiles)
            {
                var name = Path.GetFileNameWithoutExtension(pageFile.FileName);
                var type = Path.GetExtension(pageFile.FileName);

                var page = new ComicsPages
                {
                    Name = name,
                    FileType = type,
                };
                using (var dataStream = new MemoryStream())
                {
                    await pageFile.CopyToAsync(dataStream);
                    page.Content = dataStream.ToArray();
                }

                comics.Pages.Add(page);

                _context.ComicsPages.Add(page);
                _context.SaveChanges();
            }

            var comicsViewModel = _mapper.Map<ComicsViewModel>(comics);
            comicsViewModel.Comics = comics;

            return View("ComicsInfo", comicsViewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}