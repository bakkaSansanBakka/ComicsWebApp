using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace ComicsWebApp.Data.Repositories
{
    public class UnitOfWork : IDisposable
    {
        private ComicsDbContext context;
        private ComicsRepository comicsRepository;
        private ComicsPagesRepository comicsPagesRepository;
        private ComicsGenresRepository comicsGenresRepository;
        private bool disposed = false;

        public UnitOfWork(ComicsDbContext context)
        {
            this.context = context;
        }
        public ComicsRepository ComicsRepository
        {
            get
            {
                if (comicsRepository == null)
                {
                    comicsRepository = new ComicsRepository(context);
                }
                return comicsRepository;
            }
        }
        public ComicsPagesRepository ComicsPagesRepository
        {
            get
            {
                if (comicsPagesRepository == null)
                {
                    comicsPagesRepository = new ComicsPagesRepository(context);
                }
                return comicsPagesRepository;
            }
        }
        public ComicsGenresRepository ComicsGenresRepository
        {
            get
            {
                if (comicsGenresRepository == null)
                {
                    comicsGenresRepository = new ComicsGenresRepository(context);
                }
                return comicsGenresRepository;
            }
        }
        public void Save()
        {
            context.SaveChanges();
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
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
