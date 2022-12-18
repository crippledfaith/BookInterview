using FluentValidation;

namespace Book.Shared.Models
{
    public class BookValidator : AbstractValidator<Book>
    {
        public BookValidator()
        {
            CascadeMode = CascadeMode.Stop;

            RuleFor(person => person.Title).NotEmpty().WithMessage("Title is a required field.")
                .Length(3, 50).WithMessage("Title must be between 3 and 50 characters.");
            RuleFor(person => person.Author).NotEmpty().WithMessage("Author is a required field.")
                .Length(3, 50).WithMessage("Author must be between 3 and 50 characters.");
            RuleFor(person => person.ISBN).NotEmpty().WithMessage("ISBN is a required field.")
                .Length(3, 50).WithMessage("ISBN must be between 3 and 50 characters.");
        }
    }
}