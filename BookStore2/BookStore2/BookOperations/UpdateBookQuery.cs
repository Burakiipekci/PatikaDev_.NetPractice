using AutoMapper;
using BookStore2.Context;
using BookStore2.Entity;
using System;
using System.Linq;

namespace BookStore2.BookOperations
{
    public class UpdateBookQuery
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public int BookId { get; set; }
        public UpdateBookModel Model { get; set; }
        public UpdateBookQuery(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public void Handle()
        {
            var book = _context.Books.SingleOrDefault(x => x.Id == BookId);
            if (book is not null)
                throw new InvalidOperationException("Kitap yok");
            book= _mapper.Map<Book>(Model);
            _context.SaveChanges();
        }
        

    }
    public class UpdateBookModel
    {
        public string Title { get; set; }
        public int GenreId { get; set; }

        public int PageCount { get; set; }

        public DateTime PublishDate { get; set; }
    }
}
