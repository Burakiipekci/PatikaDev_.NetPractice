using AutoMapper;
using BookStore2.Context;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;

namespace BookStore2.Application.TokenOperations
{
    public class CreateTokenCommand
    {
        public CreateTokenModel Model { get; set; }
        private readonly IBookStoreDbContext _context;
        IConfiguration _configuration;
        private readonly IMapper _mapper;
        public CreateTokenCommand(IBookStoreDbContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }
        public Token Handle()
        {
            var user= _context.Users.FirstOrDefault(x=>x.Email == Model.email && x.Password == Model.password); 
            if (user is not null)
            {
                // Token Yarat
                TokenHandler handler = new TokenHandler(_configuration);
                Token token =handler.CreateAccessToken(user);
                user.RefreshToken = token.RefreshToken;
                user.RefreshTokenExpiraDate = token.Expiration.AddMinutes(5);
                _context.SaveChanges();
                return token;
                
            }
            else
            {
                throw new InvalidOperationException("Kullanıcı adı - şifre hatalı");
            }
        }

    }
    public class CreateTokenModel
    {
        public string email { get; set; }
        public string password { get; set; }
    }
}
