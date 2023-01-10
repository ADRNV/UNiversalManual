using MediatR;
using Microsoft.AspNetCore.Mvc;
using UMan.DataAccess.Entities;

namespace UMan.API.Features.Auth
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        public Task<string> Login(string login, string password)
        {
            var user = new User()
            {
                UserName = login,
                PasswordHash = password
            };

            return _mediator.Send(new Login.Command(user));
        }

        [HttpPost("register")]
        public Task<string> Register(string login, string password)
        {
            var user = new User()
            {
                UserName = login,
                PasswordHash = password
            };

            return _mediator.Send(new Register.Command(user));
        }
    }
}
