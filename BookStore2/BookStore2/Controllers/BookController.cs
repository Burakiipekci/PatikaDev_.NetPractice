using AutoMapper;
using BookStore2.BookOperations;
using BookStore2.Context;
using BookStore2.Entity;
using BookStore2.FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace BookStore2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public BookController(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }
        [HttpGet]
        public IActionResult GetBooks()
        {
            List<BooksViewModel> result;
            GetBookQuery query = new GetBookQuery(_context);
            result = query.Handle();

            return Ok(result);
        }
        [HttpPost]
        public IActionResult AddBook([FromBody] AddBookModel newBook)
        {
            List<BooksViewModel> result;
            AddBookQuery command = new AddBookQuery(_context, _mapper);

            try
            {
                command.Model = newBook;
                BookValidation validator = new BookValidation();
                ValidationResult validation = validator.Validate(command);
                if (validation.IsValid)
                    foreach (var item in validation.Errors)
                    {
                        System.Console.WriteLine("Prop." + item.PropertyName + "-- Eror Message:" + item.ErrorMessage);
                    }
                else
                {
                    command.Handle();
                }
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);

            }
            return Ok();
        }
    }
}
