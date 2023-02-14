using AutoMapper;
using BookStore2.Context;
using BookStore2.Entity;
using System;
using System.Linq;

namespace BookStore2.Application.UserOperations
{
    public class CreateUserCommand
    {
        public CreateUserModel Model { get; set; }
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateUserCommand(IMapper mapper, IBookStoreDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public void Handle()
        {
            var user= _context.Users.SingleOrDefault(p=> p.Email == Model.Email);
            if (user is not null)
                throw new InvalidOperationException("kullanıcı zaten mevcut");
            user = _mapper.Map<User>(Model);
            
        }
    }
    public class CreateUserModel
    {

    
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string LastName { get; set; }
    }
}
