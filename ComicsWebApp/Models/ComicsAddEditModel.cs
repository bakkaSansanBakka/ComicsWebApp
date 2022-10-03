using ComicsWebApp.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ComicsWebApp.Models
{
    public class ComicsAddEditModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public float Price { get; set; }
        public byte[] Cover { get; set; }
        public CoverType CoverType { get; set; }
        public ComicsLanguage Language { get; set; }
        public string Publisher { get; set; }
        public ComicsAvailabilityStatus AvailabilityStatus { get; set; }
        public int PagesNumber { get; set; }
        public string PublicationFormat { get; set; }
        public int YearOfPublication { get; set; }
        public string? Description { get; set; }

        public List<ComicsGenreDTO> Genres { get; set; } = new List<ComicsGenreDTO>();
        public List<SelectListItem> AllGenresList { get; set; }
        public int[] GenresIds { get; set; }
    }
}
