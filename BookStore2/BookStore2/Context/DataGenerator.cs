using BookStore2.Entity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace BookStore2.Context
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
            {
                if (context.Books.Any())
                {
                    return;
                }
                context.Genres.AddRange(new Genre
                {
                    Name = "Personal Growth"


                }, new Genre
                {
                    Name = "Science Fiction"
                }, new Genre
                {
                    Name = "Romance"
                }
                );

                context.Books.AddRange(
                    new Book
                    {
                        Title = "Start With Why",
                        GenreId = 1,
                        PageCount = 274,
                        PublishDate = new DateTime(2005, 1, 3)
                    },
                    new Book
                    {
                        Title = "Start With Dinner",
                        GenreId = 2,
                        PageCount = 172,
                        PublishDate = new DateTime(2004, 2, 2)
                    }
                );
                context.SaveChanges();
            }
        }
             public static List<Author> Authors
        {
            get
            {
                var authors = new List<Author>
                {
                    new Author
                    {
                        FirstName = "Ahmet",
                        LastName = "Selim",
                        DateOfBirth = DateTime.Parse("1969-06-03")
                    },
                    new Author
                    {
                        FirstName = "Necati",
                        LastName = "Tugel",
                        DateOfBirth = DateTime.Parse("1980-03-01")
                    },
                    new Author
                    {
                        FirstName = "Saliha",
                        LastName = "Selman",
                        DateOfBirth = DateTime.Parse("1960-07-11")
                    },
                };
                return authors;
            }
        }
    }
}
