using ComicsWebApp.Models;
using FluentValidation;

namespace ComicsWebApp.Utilities
{
    public class ComicsAddEditModelValidator : AbstractValidator<ComicsAddEditModel>
    {
        public ComicsAddEditModelValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty()
                .MaximumLength(200)
                .WithMessage("Entered value is too long");

            RuleFor(c => c.Author)
                .NotEmpty()
                .MaximumLength(100)
                .WithMessage("Entered value is too long");

            RuleFor(c => c.Price)
                .GreaterThan(0)
                .WithMessage("Price should be greater than 0.")
                .InclusiveBetween(0, 10000)
                .WithMessage("Price should be within the range of 0 and 10000.");

            RuleFor(c => c.CoverType)
                .IsInEnum();

            RuleFor(c => c.Language)
                .IsInEnum();

            RuleFor(c => c.Publisher)
                .NotEmpty()
                .MaximumLength(70)
                .WithMessage("Entered value is too long");

            RuleFor(c => c.AvailabilityStatus)
                .IsInEnum();

            RuleFor(c => c.PagesNumber)
                .GreaterThan(0)
                .WithMessage("Number of pages should be greater than 0.")
                .InclusiveBetween(0, 1000)
                .WithMessage("Number of pages should be within the range of 0 and 1000.");

            RuleFor(c => c.PublicationFormat)
                .NotEmpty()
                .MaximumLength(15)
                .WithMessage("Entered value is too long");

            RuleFor(c => c.YearOfPublication)
                .InclusiveBetween(1950, DateTime.Now.Year)
                .WithMessage("Year of Publication should be within the range of 1950 and {To}.");
        }
    }
}
