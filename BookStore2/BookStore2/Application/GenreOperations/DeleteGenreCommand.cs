using BookStore2.Context;
using System;
using System.Linq;

namespace BookStore2.Application.GenreOperations
{
    public class DeleteGenreCommand
    {
        public int GenreId { get; set; }

        private readonly IBookStoreDbContext _context;
        public DeleteGenreCommand(IBookStoreDbContext context)
        {
            _context = context;
        }
        public void Handle()
        {
            var genre= _context.Genres.SingleOrDefault(z=>z.Id==GenreId);
            if (genre is null)
                throw new InvalidOperationException("kitap türü bulunmadı!");

            _context.Genres.Remove(genre);
            _context.SaveChanges();
        }


    }
}
