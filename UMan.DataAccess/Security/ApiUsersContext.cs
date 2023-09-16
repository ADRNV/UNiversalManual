using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UMan.DataAccess.Entities;
using UMan.DataAccess.EntitiesConfiguration;

namespace UMan.DataAccess.Security
{
    public class ApiUsersContext : IdentityDbContext<User, UserRole, Guid, IdentityUserClaim<Guid>, IdentityUserRole<Guid>, IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, Token>
    {
        public ApiUsersContext(DbContextOptions<ApiUsersContext> options) : base(options)
        {
            this.Database.EnsureDeleted();
            this.Database.EnsureCreated();
        }

        public ApiUsersContext()
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserRoleConfiguration());

            base.OnModelCreating(builder);
        }

    }
}
