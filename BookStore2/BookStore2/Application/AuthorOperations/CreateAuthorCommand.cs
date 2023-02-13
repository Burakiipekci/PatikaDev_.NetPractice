using AutoMapper;
using BookStore2.Context;
using BookStore2.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace BookStore2.Application.AuthorOperations
{
    public class CreateAuthorCommand
    {
        public CreateAuthorModel Model { get; set; }
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateAuthorCommand(IMapper mapper, IBookStoreDbContext context, CreateAuthorModel model)
        {
            _mapper = mapper;
            _context = context;
            Model = model;
        }
        public void Handle()
        {
            var author = _context.Authors.SingleOrDefault(
                 s => s.FirstName == Model.FirstName && s.LastName == Model.LastName
             );
            if (author is not null)
                throw new InvalidOperationException("Author already added");
            author = _mapper.Map<Author>(Model);
            _context.Authors.Add(author);
            var isAdded = _context.SaveChanges();
            if (isAdded <= 0)
                throw new InvalidOperationException("An error occured while adding the author.");

        }
    }
    public class CreateAuthorModel
    {
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public DateTime DateOfBirth { get; set; }
    }
}
