using AutoMapper;
using BookStore2.Context;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookStore2.Application.AuthorOperations
{
    public class GetAuthorByIdQuery
    {
        public int Id { get; set; }
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetAuthorByIdQuery(IBookStoreDbContext context, IMapper mapper, int itemId)
        {
            _context = context;
            _mapper = mapper;
            Id = itemId;
        }
        public AuthorDetailViewModel Handle()
        {
            var author = (

                from a in _context.Authors.Where(a => a.Id == Id)
                join ba in _context.BookAuthors on a.Id equals ba.AuthorId into baGroup
                select new AuthorDetailViewModel
                {
                    Id = a.Id,
                    AuthorName = a.FirstName + " " + a.LastName,
                    DateOfBirth = a.DateOfBirth.Date.ToString("dd/MM/yyyy"),
                    Books = _context.Books
                         .Where(
                            w =>
                                w.IsPublished
                                && baGroup.Select(s => s.BookId).ToArray().Contains(w.Id)
                        )
                        .Select(
                            sm =>
                                new AuthorBooksViewModel
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

                }
            ).SingleOrDefault();

            if (author is null)
                throw new InvalidOperationException("Author not found");

            return author;
        }

    }


    public class AuthorDetailViewModel
    {
        public int Id { get; set; }
        public string AuthorName { get; set; } = "";
        public string DateOfBirth { get; set; } = "";
        public List<AuthorBooksViewModel> Books { get; set; } = null!;
    }

    public class AuthorBooksViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string Genre { get; set; } = "";
        public int PageCount { get; set; }
        public string PublishDate { get; set; } = "";
    }
}
