using AutoMapper;
using BookStore2.Application.AuthorOperations;
using BookStore2.Application.BookOperations;
using BookStore2.Application.GenreOperations;
using BookStore2.Entity;

namespace BookStore2.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AddBookModel, Book>();
            CreateMap<CreateBookModel, Book>();
            CreateMap<Book, AddBookModel>().ForMember(dest => dest.genre, opt => opt.MapFrom(src => ((GenreEnum)src.GenreId).ToString()));
            CreateMap<Genre, GenresViewModel>();
            CreateMap<Genre, GenreDetailViewModel>();



            CreateMap<CreateAuthorModel, Author>();
            CreateMap<UpdateAuthorModel, Author>();
            CreateMap<Author, AuthorsViewModel>()
                .ForMember(
                    dest => dest.DateOfBirth,
                    opt => opt.MapFrom(src => src.DateOfBirth.Date.ToString("dd/MM/yyyy"))
                );
            CreateMap<Author, AuthorDetailViewModel>()
                .ForMember(
                    dest => dest.DateOfBirth,
                    opt => opt.MapFrom(src => src.DateOfBirth.Date.ToString("dd/MM/yyyy"))
                );

            // Add book to author mapping
            CreateMap<AddBookToAuthorModel, Book>();
            //add author to book mapping
            CreateMap<AddBookToAuthorModel, Author>();




        }

    }
}
