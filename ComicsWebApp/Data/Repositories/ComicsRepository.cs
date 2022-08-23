using ComicsWebApp.Data.Models;
using Microsoft.EntityFrameworkCore;

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
    }
}
