using BookStore2.BookOperations;
using FluentValidation;
using System;

namespace BookStore2.FluentValidation
{
    public class BookValidation : AbstractValidator<AddBookQuery>

    {
        public BookValidation()
        {
            RuleFor(r => r.Model.GenreId).GreaterThan(0);
            RuleFor(r => r.Model.PageCount).GreaterThan(0);
            RuleFor(r => r.Model.PublishDate).NotEmpty().LessThan(DateTime.Now.Date);
            RuleFor(r => r.Model.Title).NotEmpty().MinimumLength(4);


        }
    }
}
