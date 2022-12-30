using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace UMan.DataAccess
{
    public class CurrentUserAccessor : ICurrentUserAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string? GetCurrentUsername()
        {
            return _httpContextAccessor.HttpContext?.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
        }
    }
}
