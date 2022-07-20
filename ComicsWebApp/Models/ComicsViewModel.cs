using Microsoft.AspNetCore.Mvc.Rendering;

namespace ComicsWebApp.Models
{
    public class ComicsViewModel
    {
        public Comics Comics { get; set; }
        public List<SelectListItem> Genres { get; set; }
        public List<ComicsGenre> ListOfGenres { get; set; }
        public int ComicsId { get; set; }
        public int[] GenresIds { get; set; }
    }
}
