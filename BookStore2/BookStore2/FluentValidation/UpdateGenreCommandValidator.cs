using BookStore2.Application.GenreOperations;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace BookStore2.FluentValidation
{
    public class UpdateGenreCommandValidator: AbstractValidator<UpdateGenreCommand>
    {
        public UpdateGenreCommandValidator()
        {
            RuleFor(command => command.Model.Name).MaximumLength(4).When(x=> x.Model.Name != string.Empty);
        }
    }
}
