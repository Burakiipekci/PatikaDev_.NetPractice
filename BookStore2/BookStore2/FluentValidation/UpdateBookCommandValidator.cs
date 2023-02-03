using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System;
using FluentValidation;
using BookStore2.BookOperations;

namespace BookStore2.FluentValidation
{
    public class UpdateBookCommandValidator:AbstractValidator<UpdateBookQuery>
    {
        public UpdateBookCommandValidator() { 
        
        RuleFor(command => command.BookId).NotEmpty().GreaterThan(0);
        RuleFor(command => (int) command.Model!.GenreId).GreaterThan(0);
        RuleFor(command => command.Model!.PageCount).GreaterThan(0);
        RuleFor(command => command.Model!.PublishDate.Date).NotEmpty().LessThan(DateTime.Now.AddDays(1).Date);
        RuleFor(command => command.Model!.Title).NotEmpty().MinimumLength(4);
        
        }
    }
}
