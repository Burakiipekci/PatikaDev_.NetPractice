﻿using AutoMapper;
using BookStore2.BookOperations;
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


        }

    }
}
