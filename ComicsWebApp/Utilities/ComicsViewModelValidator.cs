using ComicsWebApp.Models;
using FluentValidation;

namespace ComicsWebApp.Utilities
{
    public class ComicsViewModelValidator : AbstractValidator<ComicsViewModel>
    {
        public ComicsViewModelValidator()
        {
            RuleFor(c => c.Comics.Name).NotEmpty();
            RuleFor(c => c.Comics.Author).NotEmpty();
            RuleFor(c => c.Comics.Price).GreaterThan(0);
            RuleFor(c => c.Comics.Cover).NotNull();
            RuleFor(c => c.Comics.CoverType).IsInEnum();
            RuleFor(c => c.Comics.Language).IsInEnum();
            RuleFor(c => c.Comics.Publisher).NotEmpty();
            RuleFor(c => c.Comics.AvailabilityStatus).IsInEnum();
            RuleFor(c => c.Comics.PagesNumber).GreaterThan(0);
            RuleFor(c => c.Comics.PublicationFormat).NotEmpty();
            RuleFor(c => c.Comics.YearOfPublisihing).NotEmpty();

            RuleFor(c => c.GenresIds).NotNull();
        }
    }
}
