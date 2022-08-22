using ComicsWebApp.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ComicsWebApp.Data.Repositories
{
    public class ComicsPagesRepository : BaseRepository<ComicsPages>
    {
        public ComicsPagesRepository(ComicsDbContext context) : base(context) { }
    }
}
