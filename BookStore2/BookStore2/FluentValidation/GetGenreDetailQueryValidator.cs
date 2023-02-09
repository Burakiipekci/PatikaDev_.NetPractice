using BookStore2.Application.GenreOperations;
using BookStore2.Entity;
using FluentValidation;

namespace BookStore2.FluentValidation
{
    public class GetGenreDetailQueryValidator : AbstractValidator<GetGenreDetailQuery>
    {
        public GetGenreDetailQueryValidator()
        {
            RuleFor(query=> query.GenreId).GreaterThan(0);
                }
    }
}
