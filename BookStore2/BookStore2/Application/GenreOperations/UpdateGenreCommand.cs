using BookStore2.Context;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Linq;

namespace BookStore2.Application.GenreOperations
{
    public class UpdateGenreCommand
    {
        public int GenreId { get; set; }
        public UpdateGenreModel Model { get; set; }
        private readonly BookStoreDbContext _dbContext;

        public UpdateGenreCommand(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Handle()
        {
            var genre = _dbContext.Genres.SingleOrDefault(x => x.Id == GenreId);
            if (genre is null)

                throw new InvalidOperationException("Kitap türü bulunamadı");
            if (_dbContext.Genres.Any(x => x.Name.ToLower() == Model.Name.ToLower() && x.Id != GenreId))
                throw new InvalidOperationException("Kitap türü bulunamadı");

            genre.Name = Model.Name.Trim() == default ? Model.Name : genre.Name;
            genre.IsActive = Model.IsValid;
            _dbContext.SaveChanges();   

        }
    }
    public class UpdateGenreModel
    {
        public string Name { get; set; }
        public bool IsValid { get; set; }
    }
}
