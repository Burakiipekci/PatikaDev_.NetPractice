using AutoMapper;
using BookStore2.BookOperations;
using BookStore2.Entity;

namespace BookStore2.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AddBookModel, Book>();

        }

    }
}
