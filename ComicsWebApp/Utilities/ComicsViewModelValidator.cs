using ComicsWebApp.Models;
using FluentValidation;

namespace ComicsWebApp.Utilities
{
    public class ComicsViewModelValidator : AbstractValidator<ComicsViewModel>
    {
        public ComicsViewModelValidator()
        {
            RuleFor(c => c.Comics.Name)
                .NotEmpty()
                .MaximumLength(200)
                .WithMessage("Entered value is too long");

            RuleFor(c => c.Comics.Author)
                .NotEmpty()
                .MaximumLength(100)
                .WithMessage("Entered value is too long");

            RuleFor(c => c.Comics.Price)
                .GreaterThan(0)
                .WithMessage("Price should be greater than 0.")
                .InclusiveBetween(0, 10000)
                .WithMessage("Price should be within the range of 0 and 10000.");

            RuleFor(c => c.Comics.CoverType)
                .IsInEnum();

            RuleFor(c => c.Comics.Cover)
                .NotNull();

            RuleFor(c => c.Comics.Language)
                .IsInEnum();

            RuleFor(c => c.Comics.Publisher)
                .NotEmpty()
                .MaximumLength(70)
                .WithMessage("Entered value is too long");

            RuleFor(c => c.Comics.AvailabilityStatus)
                .IsInEnum();

            RuleFor(c => c.Comics.PagesNumber)
                .GreaterThan(0)
                .WithMessage("Number of pages should be greater than 0.")
                .InclusiveBetween(0, 1000)
                .WithMessage("Number of pages should be within the range of 0 and 1000.");

            RuleFor(c => c.Comics.PublicationFormat)
                .NotEmpty()
                .MaximumLength(15)
                .WithMessage("Entered value is too long");

            RuleFor(c => c.Comics.YearOfPublication)
                .InclusiveBetween(1950, DateTime.Now.Year)
                .WithMessage("Year of Publication should be within the range of 1950 and {To}.");
        }
    }
}
