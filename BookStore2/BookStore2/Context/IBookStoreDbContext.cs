using BookStore2.Entity;
using Microsoft.EntityFrameworkCore;

namespace BookStore2.Context
{
    public interface IBookStoreDbContext
    {

        public DbSet<Book> Books { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }
        public int SaveChanges();
    }
}
