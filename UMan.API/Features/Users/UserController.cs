using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UMan.DataAccess.Entities;

namespace UMan.API.Features.Users
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Policy = "Administrator")]
    public class UserController
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IdentityResult> Create([FromBody] User user) =>
            await _mediator.Send(new Create.Command(user));

        [HttpDelete]
        public async Task<IdentityResult> Delete([FromBody] User user) =>
            await _mediator.Send(new Delete.Commad(user));
    }
}
