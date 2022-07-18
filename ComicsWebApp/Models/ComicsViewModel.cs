using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ComicsWebApp.Models
{
    public class ComicsViewModel
    {
        public Comics Comics { get; set; }
        public List<SelectListItem> Genres { get; set; }
        public int[] GenresIds { get; set; }
    }
}
