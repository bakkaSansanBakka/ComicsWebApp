using Microsoft.AspNetCore.Mvc.Rendering;

namespace ComicsWebApp.Models
{
    public class ComicsViewModel
    {
        public Comics Comics { get; set; }
        public List<SelectListItem> AllGenresList { get; set; }
        public List<ComicsGenre> ListOfGenres { get; set; }
        public List<ComicsPages> ListOfPages { get; set; }
        public int[] GenresIds { get; set; }
    }
}
