using AutoMapper;
using BookStore2.Context;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace BookStore2.Application.AuthorOperations
{
    public class DeleteAuthorCommand 
    {
        private readonly BookStoreDbContext _context;

      
        public int Id { get; set; }

        public DeleteAuthorCommand(BookStoreDbContext context, int itemId)
        {
            _context = context;
            Id = itemId;
        }
        public void Handle() {

            var author = _context.Authors.SingleOrDefault(s => s.Id == Id);
            if (author is null)
                throw new InvalidOperationException("Author not found");
            var authorBooksCheck = (
             from ab in _context.BookAuthors.Where(w => w.AuthorId == author.Id)
             from b in _context.Books.Where(w => w.Id == ab.BookId)
             select b
         ).ToList();
            if (authorBooksCheck.Count() > 0)
            {
                throw new InvalidOperationException(
                    $"The author you are trying to delete has {authorBooksCheck.Count()} book(s) in publication. Please delete the books first."
                );
            }

            _context.Remove(author);
            _context.SaveChanges();



        }


    }
}
