using AutoMapper;
using BookStore2.Application.BookOperations;
using BookStore2.Context;
using BookStore2.Entity;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests
{
    public class CreateBookCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public CreateBookCommandTests(CommonTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }
        [Fact]
        public void WhenAlreadyExistBookTitleIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            // Arrange
            var book = new Book() { Title = "test", PageCount = 1, PublishDate = new DateTime(1990, 01, 10), GenreId = 1 };
            _context.Books.Add(book);
            _context.SaveChanges();
            CreateBookCommand cmd = new CreateBookCommand(_context, _mapper);
            cmd.Model = new CreateBookModel() { Title = book.Title };


            //Act & Assert
            FluentActions.Invoking(() => cmd.Hande()).Should()
                .Throw<InvalidOperationException>().And.Message
                .Should().Be("Kitap zaten mevcut");

        }
    }
}
