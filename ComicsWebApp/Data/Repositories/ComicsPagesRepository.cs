using ComicsWebApp.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ComicsWebApp.Data.Repositories
{
    public class ComicsPagesRepository : IRepository<ComicsPages>
    {
        private ComicsDbContext _context;
        private bool disposed = false;

        public ComicsPagesRepository(ComicsDbContext context)
        {
            _context = context;
        }
        public IEnumerable<ComicsPages> GetAll()
        {
            return _context.ComicsPages;
        }
        public ComicsPages GetById(int id)
        {
            return _context.ComicsPages.Find(id);
        }
        public void Create(ComicsPages comicsPage)
        {
            _context.ComicsPages.Add(comicsPage);
            _context.SaveChanges();
        }
        public void Update(ComicsPages comicsPage)
        {
            _context.Entry(comicsPage).State = EntityState.Modified;
            _context.SaveChanges();
        }
        public void Delete(int id)
        {
            var comicsPage = _context.ComicsPages.Find(id);
            if (comicsPage != null)
            {
                _context.ComicsPages.Remove(comicsPage);
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
