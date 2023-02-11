using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Net;
using System.Security.Claims;
using UMan.API.ApiModels;
using UMan.DataAccess.Entities;
using UMan.DataAccess.Security;
using UMan.DataAccess.Security.Common;

namespace UMan.API.Features.Auth
{
    public class Login
    {
        public record Command(User User) : IRequest<JwtAuthResult>;

        public class LoginByPassword : IRequestHandler<Command, JwtAuthResult>
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

            public async Task<JwtAuthResult> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByNameAsync(request.User.UserName);

                var roles = await _userManager.GetRolesAsync(user);

                var claims = new[]
                {
                    new Claim(ClaimTypes.Role, roles.First()),
                    new Claim(ClaimTypes.Name, request.User.UserName)
                };

                var siginResult = await _signInManager.PasswordSignInAsync(user, request.User.PasswordHash, false, false);

                if (siginResult.Succeeded)
                {
                    var token = await _jwtAuthManager.GenerateTokens(request.User, claims, DateTime.Now.AddMinutes(1));

                    return token;
                }
                else
                {
                    throw new RestException(HttpStatusCode.Conflict);
                }

            }
        }
    }
}
