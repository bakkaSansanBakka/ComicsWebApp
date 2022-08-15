using ComicsWebApp.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ComicsWebApp.Data.Repositories
{
    public class ComicsRepository : IRepository<Comics>
    {
        private ComicsDbContext _context;
        private bool disposed = false;

        public ComicsRepository(ComicsDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Comics> GetAll()
        {
            return _context.Comics;
        }
        public Comics GetById(int id)
        {
            return _context.Comics.Include(c => c.Genres).Include(c => c.Pages).FirstOrDefault(c => c.Id == id);
        }
        public Comics GetByName(String name)
        {
            return _context.Comics.FirstOrDefault(c => c.Name == name);
        }
        public IQueryable<ComicsGenre> GetGenresOfComics(int id)
        {
            return _context.Comics.Where(x => x.Id == id).SelectMany(x => x.Genres);
        }
        public void Create(Comics comics)
        {
            _context.Comics.Add(comics);
            _context.SaveChanges();
        }
        public void Update(Comics comics)
        {
            _context.Entry(comics).State = EntityState.Modified;
            _context.SaveChanges();
        }
        public void Delete(int id)
        {
            var comics = _context.Comics.Find(id);
            if (comics != null)
            {
                _context.Comics.Remove(comics);
                _context.SaveChanges();
            }
        }
        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
