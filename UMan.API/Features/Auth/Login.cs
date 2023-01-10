using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using UMan.DataAccess.Entities;
using UMan.DataAccess.Security.Common;

namespace UMan.API.Features.Auth
{
    public class Login
    {
        public record Command(User User) : IRequest<string>;

        public class LoginByPassword : IRequestHandler<Command, string>
        {
            private readonly IJwtAuthManager _jwtAuthManager;

            private readonly UserManager<User> _userManager;

            private readonly SignInManager<User> _signInManager;

            public LoginByPassword(IJwtAuthManager jwtAuthManager, UserManager<User> userManager, SignInManager<User> signInManager)
            {
                _jwtAuthManager = jwtAuthManager;

                _userManager = userManager;

                _signInManager = signInManager;
            }

            public async Task<string> Handle(Command request, CancellationToken cancellationToken)
            {
                var roles = await _userManager.GetRolesAsync(request.User);

                var claims = new[]
                {
                    new Claim(ClaimTypes.Role, roles[0]),
                    new Claim(ClaimTypes.Name, request.User.UserName)
                };

                var canSigin = await _signInManager.CanSignInAsync(request.User);

                if (canSigin)
                {
                    var token = await _jwtAuthManager.GenerateTokens(request.User, claims, DateTime.Now.AddMinutes(1));

                    return token.AccessToken;
                }
                else
                {
                    return null;
                }

            }
        }
    }
}
