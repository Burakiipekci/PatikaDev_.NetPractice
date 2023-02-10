using BookStore2.Application.AuthorOperations;
using FluentValidation;
using System;

namespace BookStore2.FluentValidation
{
    public class CreateAuthorCommandValidator : AbstractValidator<CreateAuthorCommand>
    {
        public CreateAuthorCommandValidator()
        {
            RuleFor(cmd => cmd.Model.FirstName).NotEmpty().MinimumLength(2);
            RuleFor(cmd => cmd.Model.LastName).NotEmpty().MinimumLength(2);
            RuleFor(cmd => cmd.Model.DateOfBirth).NotEmpty().LessThan(DateTime.Now.AddYears(-15));
        }
    }
}
