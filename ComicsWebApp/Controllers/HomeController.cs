using AutoMapper;
using ComicsWebApp.Data;
using ComicsWebApp.Data.Models;
using ComicsWebApp.Models;
using ComicsWebApp.Utilities;
using FluentValidation;
using FluentValidation.Results;
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
        private readonly IValidator<ComicsAddEditModel> _comicsAddEditModelValidator;

        public HomeController(ILogger<HomeController> logger, ComicsDbContext context, IMapper mapper, IValidator<ComicsAddEditModel> comicsAddEditModelValidator)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
            _comicsAddEditModelValidator = comicsAddEditModelValidator;
        }

        public IActionResult Index()
        {
            var listOfComicsViewModel = new ListOfComicsViewModel();
            listOfComicsViewModel.ListOfComics = _context.Comics.ToList();
            return View(listOfComicsViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult AddComics()
        {
            var comicsAddEditModel = new ComicsAddEditModel();
            comicsAddEditModel.AllGenresList = _context.ComicsGenres.Select(g => new SelectListItem { Text = g.GenreName, Value = g.Id.ToString() }).ToList();
            return View(comicsAddEditModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddComics(ComicsAddEditModel comicsAddEditModel, IFormFile coverFile)
        {
            ValidationResult result = await _comicsAddEditModelValidator.ValidateAsync(comicsAddEditModel);

            if (!result.IsValid)
            {
                result.AddToModelState(this.ModelState);
                comicsAddEditModel.AllGenresList = _context.ComicsGenres.Select(g => new SelectListItem { Text = g.GenreName, Value = g.Id.ToString() }).ToList();
                return View("AddComics", comicsAddEditModel);
            }

            comicsAddEditModel.ListOfGenres = new List<ComicsGenre>();
            comicsAddEditModel.ListOfPages = new List<ComicsPages>();

            Comics comics = new Comics
            {
                Name = comicsAddEditModel.Comics.Name,
                Author = comicsAddEditModel.Comics.Author,
                Price = comicsAddEditModel.Comics.Price,
                CoverType = comicsAddEditModel.Comics.CoverType,
                Language = comicsAddEditModel.Comics.Language,
                Publisher = comicsAddEditModel.Comics.Publisher,
                AvailabilityStatus = comicsAddEditModel.Comics.AvailabilityStatus,
                PagesNumber = comicsAddEditModel.Comics.PagesNumber,
                PublicationFormat = comicsAddEditModel.Comics.PublicationFormat,
                YearOfPublication = comicsAddEditModel.Comics.YearOfPublication,
                Description = comicsAddEditModel.Comics.Description,
            };

            if (comicsAddEditModel.GenresIds.Length > 0)
            {
                foreach (var genreid in comicsAddEditModel.GenresIds)
                {
                    var comicsGenre = _context.ComicsGenres.FirstOrDefault(g => g.Id == genreid);
                    comics.Genres.Add(comicsGenre);
                    comicsAddEditModel.ListOfGenres.Add(comicsGenre);
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

            comicsAddEditModel.Comics.Id = _context.Comics.FirstOrDefault(c => c.Name == comics.Name).Id;

            var comicsResponseViewModel = _mapper.Map<ComicsViewModel>(comicsAddEditModel);

            return View("ComicsInfo", comicsResponseViewModel);
        }

        public ActionResult ComicsInfo(int id)
        {
            var comicsViewModel = new ComicsViewModel();
            comicsViewModel.ListOfGenres = new List<ComicsGenre>();
            comicsViewModel.ListOfPages = new List<ComicsPages>();

            comicsViewModel.Comics = _context.Comics.FirstOrDefault(c => c.Id == id);

            var listOfGenres = _context.Comics.Where(x => x.Id == id).SelectMany(x => x.Genres);
            foreach (var genre in listOfGenres)
            {
                comicsViewModel.ListOfGenres.Add(genre);
            }

            comicsViewModel.ListOfPages = _context.ComicsPages.Include(p => p.Comics).Where(p => p.ComicsId == id).ToList();

            return View(comicsViewModel);
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
            var comics = _context.Comics.Include(c => c.Genres).Include(c => c.Pages).FirstOrDefault(c => c.Id == id);

            if (!pagesFiles.Equals(null))
            {
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