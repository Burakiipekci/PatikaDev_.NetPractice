using BookStore2.Application.AuthorOperations;
using FluentValidation;

namespace BookStore2.FluentValidation
{
    public class DeleteAuthorCommandValidator: AbstractValidator<DeleteAuthorCommand>
    {
        public DeleteAuthorCommandValidator()
        {
            RuleFor(x=> x.Id).NotEmpty().GreaterThan(0);
        }
    }
}
