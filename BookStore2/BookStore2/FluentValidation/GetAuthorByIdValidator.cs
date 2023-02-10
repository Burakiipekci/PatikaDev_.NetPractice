using BookStore2.Application.AuthorOperations;
using FluentValidation;

namespace BookStore2.FluentValidation
{
    public class GetAuthorByIdValidator:AbstractValidator<GetAuthorByIdQuery>
    {
        public GetAuthorByIdValidator()
        {
            RuleFor(cmd => cmd.Id).GreaterThan(0);
        }
    }
}
