using Microsoft.EntityFrameworkCore;
using UMan.DataAccess.Entities;
using UMan.DataAccess.EntitiesConfiguration;

namespace UMan.DataAccess
{
    public class PapersDbContext : DbContext
    {
        public PapersDbContext(DbContextOptions options) : base(options)
        {

        }

        public PapersDbContext()
        {

        }

        public DbSet<Paper>? Papers { get; set; }

        public DbSet<Article>? Articles { get; set; }

        public DbSet<Author>? Authors { get; set; }

        public DbSet<HashTag>? HashTags { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ArticleConfiguration());

            modelBuilder.ApplyConfiguration(new AuthorConfiguration());

            modelBuilder.ApplyConfiguration(new PaperConfiguration());
        }
    }
}
