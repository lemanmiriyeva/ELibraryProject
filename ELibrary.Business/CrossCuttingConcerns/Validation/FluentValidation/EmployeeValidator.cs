using ELibrary.Entities.Concrete;
using FluentValidation;

namespace ELibrary.Business.CrossCuttingConcerns.Validation.FluentValidation
{
    public class EmployeeValidator : AbstractValidator<Employee>
    {
        public EmployeeValidator()
        {
            string message = "This column should not be empty!";
            RuleFor(b => b.FirstName).NotEmpty().Must(NotStartWith).WithMessage(message);
            RuleFor(b => b.LastName).NotEmpty().Must(NotStartWith).WithMessage(message);
            RuleFor(b => b.Age).GreaterThanOrEqualTo(18).LessThanOrEqualTo(65).NotEmpty().WithMessage(message);
            RuleFor(b => b.Position).NotEmpty().Must(NotStartWith).WithMessage(message);
            RuleFor(b => b.Salary).NotEmpty().WithMessage(message);
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