using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using UMan.API.ApiModels;
using UMan.DataAccess.Entities;
using UMan.DataAccess.Security;
using UMan.DataAccess.Security.Common;

namespace UMan.API.Features.Auth
{
    public class Register
    {
        public record Command(User User) : IRequest<JwtAuthResult>;

        public class SignUp : IRequestHandler<Command, JwtAuthResult>
        {
            private readonly IJwtAuthManager _jwtAuthManager;

            private readonly UserManager<User> _userManager;

            private readonly SignInManager<User> _signInManager;

            IPasswordHasher<User> _passwordHasher;

            public SignUp(IJwtAuthManager jwtAuthManager, UserManager<User> userManager, SignInManager<User> signInManager, IPasswordHasher<User> passwordHasher)
            {
                _jwtAuthManager = jwtAuthManager;

                _userManager = userManager;

                _signInManager = signInManager;

                _passwordHasher = passwordHasher;
            }

            public async Task<JwtAuthResult> Handle(Command request, CancellationToken cancellationToken)
            {
                if (await _userManager.FindByNameAsync(request.User.UserName) is null)
                {
                    await _userManager.CreateAsync(request.User);

                    var claims = new[]
                    {
                        new Claim(ClaimTypes.Role, "User"),
                        new Claim(ClaimTypes.Name, request.User.UserName)
                    };

                    request.User.PasswordHash = _passwordHasher.HashPassword(request.User, request.User.PasswordHash);

                    await _userManager.AddToRoleAsync(request.User, "User");

                    var token = await _jwtAuthManager.GenerateTokens(request.User, claims, DateTime.Now.AddMinutes(1));

                    await _signInManager.SignInAsync(request.User, false);

                    return token;
                }
                else
                {
                    throw new RestException(System.Net.HttpStatusCode.Conflict);
                }
            }
        }
    }
}
