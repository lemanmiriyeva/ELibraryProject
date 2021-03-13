using ELibrary.Entities.Concrete;
using FluentValidation;

namespace ELibrary.Business.CrossCuttingConcerns.Validation.FluentValidation
{
    public class BookValidator : AbstractValidator<Book>
    {
        public BookValidator()
        {
            string message = "This column should not be empty!";
            RuleFor(b => b.BookTitle).NotEmpty().Must(NotStartWith).WithMessage(message);
            RuleFor(b => b.Author).NotEmpty().Must(NotStartWith).WithMessage(message);
            RuleFor(b => b.BookCount).GreaterThanOrEqualTo(0).LessThanOrEqualTo(100).NotEmpty().WithMessage(message);
            RuleFor(b => b.Category).NotEmpty().Must(NotStartWith).WithMessage(message);
            RuleFor(b => b.RentalPrice).NotEmpty().WithMessage(message);
            RuleFor(b => b.Size).NotEmpty().GreaterThanOrEqualTo(1).LessThanOrEqualTo(1000).WithMessage(message);
            RuleFor(b => b.Language).NotEmpty().Must(NotStartWith).WithMessage(message);
        }

        public bool NotStartWith(string text)
        {
            if (text.StartsWith("Ğ") | text.StartsWith("I"))
            {
                return false;
            }

            return true;
        }
    }
}