using ComicsWebApp.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ComicsWebApp.Data.Repositories
{
    public class ComicsGenresRepository : BaseRepository<ComicsGenre>
    {
        private ComicsDbContext _context;

        public ComicsGenresRepository(ComicsDbContext context) : base(context)
        {
            _context = context;
        }
        public IQueryable<SelectListItem> GetAllAsSelectListItem()
        {
            return _context.ComicsGenres.Select(g => new SelectListItem { Text = g.GenreName, Value = g.Id.ToString() });
        }
    }
}
