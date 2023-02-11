using MediatR;
using Microsoft.AspNetCore.Mvc;
using UMan.DataAccess.Entities;
using UMan.DataAccess.Security;

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
        public async Task<JwtAuthResult> Login(string login, string password)
        {
            var user = new User()
            {
                UserName = login,
                PasswordHash = password
            };

            return await _mediator.Send(new Login.Command(user));
        }

        [HttpPost("register")]
        public async Task<JwtAuthResult> Register(string login, string password)
        {
            var user = new User()
            {
                UserName = login,
                PasswordHash = password
            };

            return await _mediator.Send(new Register.Command(user));
        }
    }
}
