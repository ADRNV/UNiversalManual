using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UMan.DataAccess.Entities;

namespace UMan.DataAccess.Security
{
    public class ApiUsersContext : IdentityDbContext<User, IdentityRole<Guid>, Guid, IdentityUserClaim<Guid>, IdentityUserRole<Guid>, IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, Token>
    {
        public ApiUsersContext(DbContextOptions<ApiUsersContext> options) : base(options)
        {
            this.Database.EnsureDeleted();
            this.Database.EnsureCreated();
        }

        public ApiUsersContext()
        {

        }
    }
}
