using Microsoft.AspNetCore.Identity;

namespace UMan.DataAccess.Entities
{
    public class User : IdentityUser<Guid>
    {
        public List<Token> Tokens { get; set; } = new();

    }
}
