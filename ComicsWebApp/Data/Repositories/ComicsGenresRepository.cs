using ComicsWebApp.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ComicsWebApp.Data.Repositories
{
    public class ComicsGenresRepository : IRepository<ComicsGenre>
    {
        private ComicsDbContext _context;
        private bool disposed = false;

        public ComicsGenresRepository(ComicsDbContext context)
        {
            _context = context;
        }
        public IEnumerable<ComicsGenre> GetAll()
        {
            return _context.ComicsGenres;
        }
        public IQueryable<SelectListItem> GetAllAsSelectListItem()
        {
            return _context.ComicsGenres.Select(g => new SelectListItem { Text = g.GenreName, Value = g.Id.ToString() });
        }
        public ComicsGenre GetById(int id)
        {
            return _context.ComicsGenres.FirstOrDefault(g => g.Id == id);
        }
        public void Create(ComicsGenre comicsGenre)
        {
            _context.ComicsGenres.Add(comicsGenre);
            _context.SaveChanges();
        }
        public void Update(ComicsGenre comicsGenre)
        {
            _context.Entry(comicsGenre).State = EntityState.Modified;
            _context.SaveChanges();
        }
        public void Delete(int id)
        {
            var comicsGenre = _context.ComicsGenres.Find(id);
            if (comicsGenre != null)
            {
                _context.ComicsGenres.Remove(comicsGenre);
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
