using Microsoft.AspNetCore.Identity;

namespace UMan.DataAccess.Entities
{
    public class Token : IdentityUserToken<Guid>
    {
        public DateTime ExpiredAt { get; set; }
    }
}
