using Microsoft.EntityFrameworkCore;
using UMan.DataAccess.Entities;
using UMan.DataAccess.EntitiesConfiguration;

namespace UMan.DataAccess
{
    public class AppDbContext : DbContext 
    {
        public DbSet<Paper>? Papers { get; set; }

        public DbSet<Article>? Articles { get; set; }

        public DbSet<Author>? Author { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ArticleConfiguration());
            
            modelBuilder.ApplyConfiguration(new AuthorConfiguration());

            modelBuilder.ApplyConfiguration(new PaperConfiguration());
        }
    }
}
