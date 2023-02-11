using MediatR;
using Microsoft.AspNetCore.Identity;
using UMan.DataAccess.Entities;

namespace UMan.API.Features.Users
{
    public class Create
    {
        public record Command(User User) : IRequest<IdentityResult>;

        public class Handler : IRequestHandler<Command, IdentityResult>
        {
            private readonly UserManager<User> _userManager;

            public Handler(UserManager<User> userManager)
            {
                _userManager = userManager;
            }

            public async Task<IdentityResult> Handle(Command request, CancellationToken cancellationToken)
            {
                return await _userManager.CreateAsync(request.User);
            }
        }
    }
}
