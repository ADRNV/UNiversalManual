using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using UMan.DataAccess.Entities;

namespace UMan.DataAccess.Security.Common
{
    public interface IJwtAuthManager
    {
        Task<JwtAuthResult> GenerateTokens(User user, Claim[] claims, DateTime now);
        Task<JwtAuthResult> Refresh(string refreshToken, string accessToken, DateTime now);
        void RemoveExpiredRefreshTokens(DateTime now);
        void RemoveRefreshTokenByUserName(string userName);
        (ClaimsPrincipal, JwtSecurityToken) DecodeJwtToken(string token);
    }
}
