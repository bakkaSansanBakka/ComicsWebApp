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
}
