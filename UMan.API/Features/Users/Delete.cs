using MediatR;
using Microsoft.AspNetCore.Identity;
using UMan.DataAccess.Entities;

namespace UMan.API.Features.Users
{
    public class Delete
    {
        public record Commad(User User) : IRequest<IdentityResult>;

        public class Handler : IRequestHandler<Commad, IdentityResult>
        {
            private UserManager<User> _userManager; 
            
            public Handler(UserManager<User> userManager)
            {
                _userManager = userManager;
            }

            public async Task<IdentityResult> Handle(Commad request, CancellationToken cancellationToken)
            {
                return await _userManager.DeleteAsync(request.User);
            }
        }
    }
}
