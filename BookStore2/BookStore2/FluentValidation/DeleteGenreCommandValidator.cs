using BookStore2.Application.GenreOperations;
using FluentValidation;

namespace BookStore2.FluentValidation
{
    public class DeleteGenreCommandValidator: AbstractValidator<DeleteGenreCommand>
    {
        public DeleteGenreCommandValidator()
        {
                RuleFor(x=> x.GenreId).NotEmpty().GreaterThan(0);
        }
    }
}
