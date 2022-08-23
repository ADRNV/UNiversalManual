using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMan.DataAccess.Tests
{
    internal class StandartFixture : IDisposable
    {
        public StandartFixture()
        {
            const string connectionString =
            @"Server=localhost;Database=UManDb;Integrated Security=true;";

            DbContextOptions<AppDbContext> options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlServer(connectionString)
                .Options;

            this.Context = new AppDbContext(options);
            this.Context.Database.EnsureDeleted();
            this.Context.Database.EnsureCreated();

            MapperConfiguration configuration = new((c) =>
            {
                c.AddProfile<AuthorMapperProfile>();
                c.AddProfile<PaperMapperProfile>();
            });

            this.Mapper = new Mapper(configuration);
        }

        public AppDbContext Context { get; }

        public IMapper Mapper { get; }
        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
