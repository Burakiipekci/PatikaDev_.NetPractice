using BookStore2.Context;
using BookStore2.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.UnitTests
{
    public static class Genres
    {
        public static void AddGenres(this BookStoreDbContext context)
        {
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
        }
    }
}
