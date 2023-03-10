using AutoMapper;
using BookStore2.Context;
using BookStore2.Entity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace BookStore2.Application.BookOperations
{
    public class UpdateBookQuery
    {
        private readonly IBookStoreDbContext _context;

        public int BookId { get; set; }
        public UpdateBookModel Model { get; set; }
        public UpdateBookModel updateBook = new UpdateBookModel();
        public UpdateBookQuery(IBookStoreDbContext context)
        {
            _context = context;

        }
        public void Handle()
        {
            var book = _context.Books.SingleOrDefault(x => x.Id == BookId);
            if (book is not null)
                throw new InvalidOperationException("Kitap yok");
            book.PublishDate = updateBook.PublishDate != default ? updateBook.PublishDate : book.PublishDate;
            book.GenreId = updateBook.GenreId != default ? updateBook.GenreId : book.GenreId;
            book.Title = updateBook.Title != default ? updateBook.Title : book.Title;
            book.PageCount = updateBook.PageCount != default ? updateBook.PageCount : book.PageCount;
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
