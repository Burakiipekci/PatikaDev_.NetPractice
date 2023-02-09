using BookStore2.Entity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace BookStore2.Context
{
    public class BookStoreDbContext:DbContext
    {
        public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options) : base(options)
        {

        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Genre> Genres { get; set; }

    }
}
