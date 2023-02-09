using AutoMapper;
using BookStore2.Context;
using System;
using System.Linq;
using System.Reflection.Metadata;

namespace BookStore2.Application.GenreOperations
{
    public class CreateGenreCommand
    {
        public CreateGenreModel Model { get; set; }
        private readonly BookStoreDbContext _contex;
        private readonly IMapper _mapper;

        public CreateGenreCommand(IMapper mapper, BookStoreDbContext contex)
        {
            _mapper = mapper;
            _contex = contex;
        }
        public void Handle()
        {
            var genre= _contex.Genres.SingleOrDefault(x=> x.Name ==Model.Name); 
            if(genre is not null)
            {
                throw new InvalidOperationException("kitap türü zaten mevcut");
                genre = new Entity.Genre();
                genre.Name = Model.Name;    
                _contex.Genres.Add(genre);
                _contex.SaveChanges();
            }
        }
    }
    public class CreateGenreModel
    {
        public string Name { get; set; }
    }
}
