using System.Collections.Generic;
using System;
using AutoMapper;
using BookStore2.Context;
using System.Linq;
using BookStore2.Entity;
using Microsoft.EntityFrameworkCore;

namespace BookStore2.Application.AuthorOperations
{
    public class UpdateAuthorCommand
    {
        public UpdateAuthorModel Model { get; set; }
        public int Id { get; set; }
        private readonly IMapper _mapper;
        private readonly BookStoreDbContext _context;

        public UpdateAuthorCommand(IMapper mapper, BookStoreDbContext context, int itemId, UpdateAuthorModel model)
        {
            _mapper = mapper;
            _context = context;
            Id = itemId;
            Model = model;
        }
        public void Handle()
        {
            var author = _context.Authors.SingleOrDefault(a=> a.Id == Id);
            if (author is not null)
                throw new InvalidOperationException("Author not found");
            if (_context.Authors.Any(
                a => a.FirstName.ToLower() == Model.FirstName.ToLower() && a.LastName.ToLower() == Model.LastName.ToLower() && a.Id != Id
                )) 
            {
            throw new InvalidOperationException("Same name of Author is available. Please try another author name.");
            }
            author.FirstName = Model.FirstName;
            author.LastName = Model.LastName;
            author.DateOfBirth = Model.DateOfBirth; 
            _context.Authors.Update(author);
            if (Model.Books?.Count() > 0 && Model.Books != null)
            {
                var booksToBeAdded = new List<Book>();
                foreach (var item in Model.Books)
                {
                    try
                    {
                        var bookCheck = _context.Books.FirstOrDefault(
                            s => s.Title.ToLower() == item.Title.ToLower() || s.Id == item.Id
                        );
                        if (bookCheck != null)
                        {
                            // kitap mevcutsa yazara ekleyelim
                            // herhangi bir güncelleme yapmıyoruz
                            // güncelleme yapılacaksa kitap güncelleme kullanılabilir
                            booksToBeAdded.Add(bookCheck);
                        }
                        else
                        {
                            // kitap mevcut değilse yeni kitap oluşturalım ve yazara ekleyelim
                            bookCheck = _mapper.Map<Book>(item);
                            booksToBeAdded.Add(bookCheck);
                        }
                    }
                    catch (System.Exception)
                    {
                        throw new InvalidOperationException(
                            "The book you are trying to add to a author does not exist. Please add the book first."
                        );
                    }
                }
                foreach (var book in booksToBeAdded)
                {
                    _context.BookAuthors.Add(new BookAuthor() { Book = book, Author = author });
                }
            }
            var isAdded = _context.SaveChanges();
            if (isAdded <= 0)
                throw new InvalidOperationException("An error occured while updating the author.");
        }
    }  
    public class UpdateAuthorModel
    {
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public DateTime DateOfBirth { get; set; }
        public List<AddBookToAuthorModel>? Books { get; set; }
    }
    public class AddBookToAuthorModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public int GenreId { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
    }
}
