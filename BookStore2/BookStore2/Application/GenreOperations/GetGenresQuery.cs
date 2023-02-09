using AutoMapper;
using BookStore2.Context;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;

namespace BookStore2.Application.GenreOperations
{
    public class GetGenresQuery
    {
        public readonly IMapper _mapper;
        public readonly BookStoreDbContext _context;
        public GetGenresQuery(BookStoreDbContext contex, IMapper mapper)
        {
            _context = contex;
            _mapper = mapper;
        }
        public List<GenresViewModel> Handle()
        {
            var genres = _context.Genres.Where(x => x.IsActive).OrderBy(z => z.Id);
            List<GenresViewModel> returnObj = _mapper.Map<List<GenresViewModel>>(genres);
            return returnObj;
        }
    }
    public class GenresViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

    }
}
