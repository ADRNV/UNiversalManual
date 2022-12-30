using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UMan.DataAccess.Entities;

namespace UMan.DataAccess.EntitiesConfiguration
{
    internal class PaperConfiguration : IEntityTypeConfiguration<Paper>
    {
        public void Configure(EntityTypeBuilder<Paper> builder)
        {
            builder.HasKey(p => p.Id);

            builder.HasMany(p => p.Articles)
                 .WithMany(a => a.Papers);

            builder.HasMany(p => p.HashTags)
                .WithMany(h => h.Papers);
        }
    }
}
