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
            RuleFor(c => c.Comics.Price).GreaterThan(0).WithMessage("Price should be greater than 0.");
            RuleFor(c => c.Comics.Cover).NotNull();
            RuleFor(c => c.Comics.CoverType).IsInEnum();
            RuleFor(c => c.Comics.Language).IsInEnum();
            RuleFor(c => c.Comics.Publisher).NotEmpty();
            RuleFor(c => c.Comics.AvailabilityStatus).IsInEnum();
            RuleFor(c => c.Comics.PagesNumber).GreaterThan(0).WithMessage("Number of pages should be greater than 0.");
            RuleFor(c => c.Comics.PublicationFormat).NotEmpty();
            RuleFor(c => c.Comics.YearOfPublisihing).GreaterThan(0).WithMessage("Year of publication should be greater than 0.");
        }
    }
}
