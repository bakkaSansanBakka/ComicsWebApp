using ComicsWebApp.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ComicsWebApp.Data.Repositories
{
    public abstract class BaseRepository<T> : IRepository<T> where T: class 
    {
        private ComicsDbContext _context;
        private bool disposed = false;

        public BaseRepository(ComicsDbContext context)
        {
            _context = context;
        }
        public List<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }
        public virtual T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }
        public void Create(T item)
        {
            _context.Set<T>().Add(item);
        }
        public void Update(T item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }
        public void Delete(int id)
        {
            var item = _context.Set<T>().Find(id);
            if (item != null)
            {
                _context.Set<T>().Remove(item);
            }
        }
        public void Save()
        {
            _context.SaveChanges();
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
