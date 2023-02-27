using AutoMapper;
using BookStore2.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Linq;

namespace BookStore2.Application.AuthorOperations
{
    public class GetAuthorsQuery
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetAuthorsQuery(IMapper mapper, IBookStoreDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public List<AuthorsViewModel> Handle()
        {
            var bookList = (
            from a in _context.Authors
            join ba in _context.BookAuthors on a.Id equals ba.AuthorId into baGroup
            select new AuthorsViewModel
            {
                Id = a.Id,
                AuthorName = a.FirstName + ' ' + a.LastName,
                DateOfBirth = a.DateOfBirth.Date.ToString("dd/MM/yyyy"),
                Books = _context.Books
                    .Where(
                        w =>
                            w.IsPublished
                            && baGroup.Select(s => s.BookId).ToArray().Contains(w.Id)
                    )
                    .Select(
                        sm =>
                            new AuthorsBooksViewModel
                            {
                                Id = sm.Id,
                                Title = sm.Title,
                                PageCount = sm.PageCount,
                                PublishDate = sm.PublishDate.Date.ToString("dd/MM/yyyy"),
                                Genre =
                                    _context.Genres
                                        .Where(f => f.Id == sm.GenreId && f.IsActive)
                                        .Select(s => s.Name)
                                        .FirstOrDefault()
                            }
                    ).ToList()
            }).ToList();
           

            return bookList;
        }
    }
    public class AuthorsViewModel
    {
        public int Id { get; set; }
        public string AuthorName { get; set; } = "";
        public string DateOfBirth { get; set; } = "";
        public List<AuthorsBooksViewModel>? Books { get; set; } = null;
    }
    public class AuthorsBooksViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string Genre { get; set; } = "";
        public int PageCount { get; set; }
        public string PublishDate { get; set; } = "";
    }
}

