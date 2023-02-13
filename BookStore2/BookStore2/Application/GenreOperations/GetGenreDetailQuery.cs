using AutoMapper;
using BookStore2.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookStore2.Application.GenreOperations
{
    public class GetGenreDetailQuery
    {
        public int GenreId { get; set; }
        public readonly IMapper _mapper;
        public readonly IBookStoreDbContext _context;
        public GetGenreDetailQuery(IBookStoreDbContext contex, IMapper mapper)
        {
            _context = contex;
            _mapper = mapper;
        }
        public GenreDetailViewModel Handle()
        {
            var genres = _context.Genres.Where(x => x.IsActive && x.Id==GenreId).OrderBy(z => z.Id);
            if (genres is null)
                throw new InvalidOperationException("Kitap türü bulunamadı");
         
            return _mapper.Map<GenreDetailViewModel>(genres);
        }
    }
    public class GenreDetailViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

    }



}


