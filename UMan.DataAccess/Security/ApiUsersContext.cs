using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UMan.DataAccess.Entities;

namespace UMan.DataAccess.Security
{
    public class ApiUsersContext : IdentityDbContext<User, UserRole, Guid>
    {
        public ApiUsersContext(DbContextOptions<ApiUsersContext> options) : base(options)
        {
            this.Database.EnsureCreated();
        }

        public ApiUsersContext()
        {

        }
    }
}
