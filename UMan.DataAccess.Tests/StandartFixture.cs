using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;

namespace UMan.DataAccess.Tests
{
    internal class StandartFixture : IDisposable
    {
        public StandartFixture()
        {
            const string connectionString =
            @"Server=localhost;Database=UManDb;Integrated Security=true;";

            DbContextOptions<PapersDbContext> options = new DbContextOptionsBuilder<PapersDbContext>()
                .UseSqlServer(connectionString)
                .Options;

            this.Context = new PapersDbContext(options);
            this.Context.Database.EnsureDeleted();
            this.Context.Database.EnsureCreated();

            MapperConfiguration configuration = new((c) =>
            {
                c.AddProfile<AuthorMapperProfile>();
                c.AddProfile<PaperMapperProfile>();
            });

            this.Mapper = new Mapper(configuration);
        }

        public PapersDbContext Context { get; }

        public IMapper Mapper { get; }
        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
