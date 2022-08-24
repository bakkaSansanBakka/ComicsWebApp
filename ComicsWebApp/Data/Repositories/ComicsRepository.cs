using ComicsWebApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ComicsWebApp.Data.Repositories
{
    public class ComicsRepository : BaseRepository<Comics>
    {
        private ComicsDbContext _context;

        public ComicsRepository(ComicsDbContext context) : base(context)
        {
            _context = context;
        }
        public override Comics GetById(int id)
        {
            return _context.Comics.Include(c => c.Genres).Include(c => c.Pages).FirstOrDefault(c => c.Id == id);
        }
        public Comics GetByName(String name)
        {
            return _context.Comics.FirstOrDefault(c => c.Name == name);
        }
        public List<Comics> GetAllMatchingSearch(string searchString)
        {
            return _context.Comics.Where(c => c.Name.Contains(searchString) 
                || c.Publisher.Contains(searchString) 
                || c.Description.Contains(searchString)).ToList();
        }
        public IQueryable<ComicsGenre> GetGenresOfComics(int id)
        {
            return _context.Comics.Where(x => x.Id == id).SelectMany(x => x.Genres);
        }

        public List<Comics> OrderByName()
        {
            return _context.Comics.OrderBy(c => c.Name).ToList();
        }

        public List<Comics> OrderByIdDescending()
        {
            return _context.Comics.OrderByDescending(c => c.Id).ToList();
        }

        public List<Comics> OrderByPrice()
        {
            return _context.Comics.OrderBy(c => c.Price).ToList();
        }

        public List<Comics> OrderByPriceDescending()
        {
            return _context.Comics.OrderByDescending(c => c.Price).ToList();
        }
    }
}
