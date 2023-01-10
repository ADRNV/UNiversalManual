using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using UMan.DataAccess.Entities;
using UMan.DataAccess.Security.Common;

namespace UMan.API.Features.Auth
{
    public class Register
    {
        public record Command(User User) : IRequest<string>;

        public class SignUp : IRequestHandler<Command, string>
        {
            private readonly IJwtAuthManager _jwtAuthManager;

            private readonly UserManager<User> _userManager;

            private readonly SignInManager<User> _signInManager;

            public SignUp(IJwtAuthManager jwtAuthManager, UserManager<User> userManager, SignInManager<User> signInManager)
            {
                _jwtAuthManager = jwtAuthManager;

                _userManager = userManager;

                _signInManager = signInManager;
            }

            public async Task<string> Handle(Command request, CancellationToken cancellationToken)
            {
                await _userManager.CreateAsync(request.User);

                var claims = new[]
                {
                    new Claim(ClaimTypes.Role, "User"),
                    new Claim(ClaimTypes.Name, request.User.UserName)
                };


                var token = await _jwtAuthManager.GenerateTokens(request.User, claims, DateTime.Now.AddMinutes(1));
                await _userManager.AddToRoleAsync(request.User, "User");
                await _signInManager.SignInAsync(request.User, false);

                return token.AccessToken;
            }
        }
    }
}
