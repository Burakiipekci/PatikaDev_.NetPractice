using AutoMapper;
using BookStore2.BookOperations;
using BookStore2.Context;
using BookStore2.Entity;
using BookStore2.FluentValidation;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Runtime.Intrinsics;

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
        public IActionResult AddBook([FromBody] CreateBookModel newBook)
        {
            CreateBookCommand command = new CreateBookCommand(_context, _mapper);
                      
            command.Model = newBook;
            
            CreateBookCommandValidator validator = new CreateBookCommandValidator();   
            validator.ValidateAndThrow(command);           
            command.Hande();
            return Ok(command);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateBook([FromRoute] int id, [FromBody] UpdateBookModel updatedBook)
        {
            UpdateBookQuery command = new UpdateBookQuery(_context);
            try
            {
                command.BookId = id;
                command.Model = updatedBook;
                UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
                validator.ValidateAndThrow(command);
                command.Handle();
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
            return Ok();


        }
    }
}


