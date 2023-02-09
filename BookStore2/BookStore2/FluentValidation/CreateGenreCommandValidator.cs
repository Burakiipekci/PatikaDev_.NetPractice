using BookStore2.Application.GenreOperations;
using FluentValidation;

namespace BookStore2.FluentValidation
{
    public class CreateGenreCommandValidator: AbstractValidator<CreateGenreCommand>
    {
        public CreateGenreCommandValidator()
        {
            RuleFor(command => command.Model.Name).NotEmpty().MaximumLength(4);
        }
    }
}
