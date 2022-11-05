using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace UMan.DataAccess.Security
{
    public class ApiUsersContext : IdentityDbContext<IdentityUser>
    {
        public ApiUsersContext(DbContextOptions<ApiUsersContext> options) : base(options)
        {
         
        }
    }
}
