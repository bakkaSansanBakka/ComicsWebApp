using AutoMapper;
using ComicsWebApp.Data;
using ComicsWebApp.Data.Models;
using ComicsWebApp.Data.Repositories;
using ComicsWebApp.Models;
using ComicsWebApp.Utilities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.CodeModifier.CodeChange;
using System.Diagnostics;
using System.IO.Pipelines;

namespace ComicsWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly string imageFileType = "image/jpg";
        private readonly ILogger<HomeController> _logger;
        private UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<ComicsAddEditModel> _comicsAddEditModelValidator;

        public HomeController(ILogger<HomeController> logger, ComicsDbContext context, IMapper mapper, IValidator<ComicsAddEditModel> comicsAddEditModelValidator)
        {
            _logger = logger;
            _mapper = mapper;
            _comicsAddEditModelValidator = comicsAddEditModelValidator;

            _unitOfWork = new UnitOfWork(context);
        }

        public async Task<IActionResult> Index(string searchString, string sortOrder, int? page = 1)
        {
            ViewData["CurrentFilter"] = searchString;

            if (page != null && page < 1)
            {
                page = 1;
            }
            var pageSize = 15;

            var listOfComics = _unitOfWork.ComicsRepository.OrderByIdDescending(page, pageSize);

            switch (sortOrder)
            {
                case "nameAscending":
                    listOfComics = _unitOfWork.ComicsRepository.OrderByName(page, pageSize);
                    ViewData["SortedBy"] = "by name";
                    break;
                case "priceDescending":
                    listOfComics = _unitOfWork.ComicsRepository.OrderByPriceDescending(page, pageSize);
                    ViewData["SortedBy"] = "by price descending";
                    break;
                case "priceAscending":
                    listOfComics = _unitOfWork.ComicsRepository.OrderByPrice(page, pageSize);
                    ViewData["SortedBy"] = "by price ascending";
                    break;
                default:
                    listOfComics = _unitOfWork.ComicsRepository.OrderByIdDescending(page, pageSize);
                    ViewData["SortedBy"] = "";
                    break;
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                listOfComics = _unitOfWork.ComicsRepository.GetAllMatchingSearch(searchString, page, pageSize);
            }

            return View(listOfComics);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult AddComics()
        {
            var comicsAddEditModel = new ComicsAddEditModel
            {
                AllGenresList = _unitOfWork.ComicsGenresRepository.GetAllAsSelectListItem().ToList()
            };
            return View(comicsAddEditModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddComics(ComicsAddEditModel comicsAddEditModel, IFormFile coverFile)
        {
            ValidationResult result = await _comicsAddEditModelValidator.ValidateAsync(comicsAddEditModel);

            if (!result.IsValid)
            {
                result.AddToModelState(this.ModelState);
                comicsAddEditModel.AllGenresList = _unitOfWork.ComicsGenresRepository.GetAllAsSelectListItem().ToList();
                return View("AddComics", comicsAddEditModel);
            }

            var comics = _mapper.Map<Comics>(comicsAddEditModel);

            if (comicsAddEditModel.GenresIds.Length > 0)
            {
                foreach (var genreid in comicsAddEditModel.GenresIds)
                {
                    var comicsGenre = _unitOfWork.ComicsGenresRepository.GetById(genreid);
                    comics.Genres.Add(comicsGenre);
                    comicsAddEditModel.Genres.Add(comicsGenre);
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

            _unitOfWork.ComicsRepository.Create(comics);
            _unitOfWork.Save();

            comicsAddEditModel.Id = _unitOfWork.ComicsRepository.GetByName(comics.Name).Id;

            var comicsResponseViewModel = _mapper.Map<ComicsViewModel>(comicsAddEditModel);

            return View("ComicsInfo", comicsResponseViewModel);
        }

        public ActionResult ComicsInfo(int id)
        {
            var comics = _unitOfWork.ComicsRepository.GetById(id);
            var comicsViewModel = _mapper.Map<ComicsViewModel>(comics);
            return View(comicsViewModel);
        }

        public ActionResult RenderPhoto(int id)
        {
            byte[] cover;
            var comics = _unitOfWork.ComicsRepository.GetById(id);
            if (comics != null)
            {
                cover = comics.Cover;
                return File(cover, imageFileType);
            }
            return View("Index");
        }

        public ActionResult RenderPage(int id)
        {
            byte[] page;
            var comicsPage = _unitOfWork.ComicsPagesRepository.GetById(id);
            if (comicsPage != null)
            {
                page = comicsPage.Content;
                return File(page, imageFileType);
            }
            return View("Index");
        }

        [HttpPost]
        public async Task<IActionResult> AddPages(List<IFormFile> pagesFiles, int id)
        {
            var comics = _unitOfWork.ComicsRepository.GetById(id);

            if (pagesFiles != null)
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

                    _unitOfWork.ComicsPagesRepository.Create(page);
                    _unitOfWork.Save();
                }
            }

            var comicsViewModel = _mapper.Map<ComicsViewModel>(comics);

            return View("ComicsInfo", comicsViewModel);
        }

        public async Task<IActionResult> DeleteComics(int id)
        {
            var comics = _unitOfWork.ComicsRepository.GetById(id);
            if (comics != null)
            {
                _unitOfWork.ComicsRepository.Delete(id);
                await _unitOfWork.SaveAsync();
                return RedirectToAction("Index");
            }
            else
                return NotFound();
        }

        public IActionResult EditComics(int id)
        {
            List<int> genresIds = new List<int>();
            var comicsEdit = _unitOfWork.ComicsRepository.GetById(id);
            comicsEdit.Genres.ToList().ForEach(result => genresIds.Add(result.Id));

            var comicsAddEditModel = _mapper.Map<ComicsAddEditModel>(comicsEdit);
            comicsAddEditModel.AllGenresList = _unitOfWork.ComicsGenresRepository.GetAllAsSelectListItem().ToList();
            comicsAddEditModel.GenresIds = genresIds.ToArray();

            return View(comicsAddEditModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditComics(ComicsAddEditModel comicsAddEditModel, IFormFile coverFile)
        {
            ValidationResult result = await _comicsAddEditModelValidator.ValidateAsync(comicsAddEditModel);

            if (!result.IsValid)
            {
                result.AddToModelState(this.ModelState);
                comicsAddEditModel.AllGenresList = _unitOfWork.ComicsGenresRepository.GetAllAsSelectListItem().ToList();
                return View(comicsAddEditModel);
            }

            var comics = _unitOfWork.ComicsRepository.GetByIdNoTracking(comicsAddEditModel.Id);

            if (comicsAddEditModel.GenresIds.Length > 0)
            {
                _unitOfWork.ComicsRepository.ClearGenres(comics.Id);
                foreach (var genreid in comicsAddEditModel.GenresIds)
                {
                    var comicsGenre = _unitOfWork.ComicsGenresRepository.GetById(genreid);
                    comicsAddEditModel.Genres.Add(comicsGenre);
                    _unitOfWork.ComicsRepository.AddGenres(comics.Id, genreid);
                }
            }

            if (coverFile != null)
            {
                if (coverFile.Length > 0)
                {
                    using (var target = new MemoryStream())
                    {
                        coverFile.CopyTo(target);
                        comicsAddEditModel.Cover = target.ToArray();
                    }
                }
            }
            else
            {
                comicsAddEditModel.Cover = comics.Cover;
            }

            comics = _mapper.Map<Comics>(comicsAddEditModel);

            _unitOfWork.ComicsRepository.Update(comics);
            _unitOfWork.Save();

            var comicsResponseViewModel = _mapper.Map<ComicsViewModel>(comicsAddEditModel);

            Console.WriteLine(comics.Cover.Length);

            return View("ComicsInfo", comicsResponseViewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}