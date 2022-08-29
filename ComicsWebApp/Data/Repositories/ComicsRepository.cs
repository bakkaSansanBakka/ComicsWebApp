using ComicsWebApp.Data.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Printing;
using System.Linq;
using X.PagedList;

namespace ComicsWebApp.Data.Repositories
{
    public class ComicsRepository : BaseRepository<Comics>
    {
        private ComicsDbContext _context;

        public ComicsRepository(ComicsDbContext context) : base(context)
        {
            _context = context;
        }
        public Comics GetByIdNoTracking(int id)
        {
            return _context.Comics.AsNoTracking().Include(c => c.Genres).Include(c => c.Pages).FirstOrDefault(c => c.Id == id);
        }
        public override Comics GetById(int id)
        {
            return _context.Comics.Include(c => c.Genres).Include(c => c.Pages).FirstOrDefault(c => c.Id == id);
        }
        public Comics GetByName(String name)
        {
            return _context.Comics.FirstOrDefault(c => c.Name == name);
        }
        public IPagedList<Comics> GetAllMatchingSearch(string searchString, int? page, int pageSize)
        {
            return _context.Comics.Where(c => c.Name.Contains(searchString) 
                || c.Publisher.Contains(searchString) 
                || c.Description.Contains(searchString)).ToPagedList(page ?? 1, pageSize);
        }
        public IQueryable<ComicsGenre> GetGenresOfComics(int id)
        {
            return _context.Comics.Where(x => x.Id == id).SelectMany(x => x.Genres);
        }

        public void ClearGenres(int id)
        {
            var comicsGenres = _context.ComicsComicsGenres.Where(g => g.ComicsId == id);
            _context.ComicsComicsGenres.RemoveRange(comicsGenres);
        }

        public void AddGenres(int comicsId, int genreId)
        {
             var comicsComicsGenre = new ComicsComicsGenres { ComicsId = comicsId, GenresId = genreId };

            _context.ComicsComicsGenres.Add(comicsComicsGenre);
        }

        public IPagedList<Comics> OrderByName(int? page, int pageSize)
        {
            return _context.Comics.OrderBy(c => c.Name).ToPagedList(page ?? 1, pageSize);
        }

        public IPagedList<Comics> OrderByIdDescending(int? page, int pageSize)
        {
            return _context.Comics.OrderByDescending(c => c.Id).ToPagedList(page ?? 1, pageSize);
        }
        public IPagedList<Comics> OrderByPrice(int? page, int pageSize)
        {
            return _context.Comics.OrderBy(c => c.Price).ToPagedList(page ?? 1, pageSize);
        }

        public IPagedList<Comics> OrderByPriceDescending(int? page, int pageSize)
        {
            return _context.Comics.OrderByDescending(c => c.Price).ToPagedList(page ?? 1, pageSize);
        }
    }
}
