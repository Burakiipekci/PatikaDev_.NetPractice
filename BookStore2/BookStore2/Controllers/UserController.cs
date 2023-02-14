using AutoMapper;

using BookStore2.Application.AuthorOperations;
using BookStore2.Application.BookOperations;
using BookStore2.Application.TokenOperations;
using BookStore2.Application.UserOperations;
using BookStore2.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace BookStore2.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        readonly IConfiguration _congfiguration;

        public UserController(IConfiguration congfiguration, IMapper mapper, IBookStoreDbContext context)
        {
            _congfiguration = congfiguration;
            _mapper = mapper;
            _context = context;
        }
        [HttpPost]
        public IActionResult Create([FromBody] CreateUserModel newUser)
        {
            CreateUserCommand cmd = new CreateUserCommand(_mapper,_context );
            cmd.Model = newUser;
            cmd.Handle();
            return Ok();
            
        }
        [HttpPost("token")]
        public ActionResult<Token> CreateToken([FromBody] CreateTokenModel login)
        {
            CreateTokenCommand cmd = new CreateTokenCommand(_context, _mapper,_congfiguration);
            cmd.Model = login;
            var token = cmd.Handle();
            return token;
        }
        [HttpGet("refresh")]
        public ActionResult<Token> CreateToken([FromBody] string token)
        {
            RefreshTokenCommand cmd = new RefreshTokenCommand(_context, _congfiguration);
            cmd.RefreshToken = token;
            var resultToken = cmd.Handle();
            return resultToken;
        }
    }
}
