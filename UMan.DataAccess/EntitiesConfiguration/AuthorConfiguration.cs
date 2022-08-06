using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UMan.DataAccess.Entities;

namespace UMan.DataAccess.EntitiesConfiguration
{
    internal class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.HasMany((a) => a.Papers)
                .WithOne((p) => p.Author);
        }
    }
}
