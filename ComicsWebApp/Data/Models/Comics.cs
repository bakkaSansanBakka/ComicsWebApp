using System.ComponentModel.DataAnnotations;

namespace ComicsWebApp.Data.Models
{

    public enum ComicsLanguage
    {
        English,
        French,
        Russian,
        Spanish,
        Italian,
        Chinese,
        Japanese,
        Deutch
    }

    public enum ComicsAvailabilityStatus
    {
        [Display(Name = "In Stock")]
        InStock,
        [Display(Name = "Sold Out")]
        SoldOut,
        [Display(Name = "On Order")]
        OnOrder
    }

    public enum CoverType
    {
        Hard,
        Soft
    }

    public class Comics
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

        public List<ComicsGenre> Genres { get; set; } = new List<ComicsGenre>();
        public List<ComicsPages> Pages { get; set; } = new List<ComicsPages>();
    }
}
