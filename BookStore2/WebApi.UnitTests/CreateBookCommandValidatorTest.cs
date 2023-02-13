using BookStore2.Application.BookOperations;
using BookStore2.FluentValidation;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace WebApi.UnitTests
{
    public class CreateBookCommandValidatorTest
    {
        [Theory]
        [InlineData("Lord of the Rings",0,0)]
        [InlineData("Lord of the Rings",0,1)]
        [InlineData(".",0,0)]
        [InlineData(".",100,1)]
        [InlineData("lor",100,1)]
        [InlineData("lord",100,0)]
        [InlineData("lord",100,1)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string title, int pageCount, int genreId)
        {
            //Arrange
            CreateBookCommand cmd = new CreateBookCommand(null, null);
            cmd.Model= new CreateBookModel()
            {
                Title = title, GenreId= genreId ,PageCount= pageCount,PublishDate = DateTime.Now.Date.AddYears(-1),
            };
            
            //Act
            CreateBookCommandValidator val= new CreateBookCommandValidator();
            var result=val.Validate(cmd);   

            //Assert

            result.Errors.Count.Should().BeGreaterThan(0);  

        }

    }
}
