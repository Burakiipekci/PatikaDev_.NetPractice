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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Book<->Author bağlantısı
            // Bir kitabın birden fazla yazarı olabilir
            // Ve tabiki bir yazarın birden fazla kitabı olabilir
            // şeklinde kurgulanmıştır.

            modelBuilder.Entity<BookAuthor>().HasKey(ba => new { ba.BookId, ba.AuthorId });
        }
        public DbSet<Book> Books { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }

    }
}
