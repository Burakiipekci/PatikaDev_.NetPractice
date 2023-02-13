using BookStore2.Context;
using BookStore2.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.UnitTests
{
    public static class Books
    {
        public static void AddBook(this BookStoreDbContext context)
        {
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

        }
    }
}
