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
        private readonly IBookStoreDbContext _dbContext;

        public UpdateGenreCommand(IBookStoreDbContext dbContext)
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

            genre.Name = string.IsNullOrEmpty(Model.Name.Trim()) == default ? genre.Name : Model.Name;
            _dbContext.SaveChanges();
           

        }
    }
    public class UpdateGenreModel
    {
        public string Name { get; set; }
        public bool IsValid { get; set; }
    }
}
