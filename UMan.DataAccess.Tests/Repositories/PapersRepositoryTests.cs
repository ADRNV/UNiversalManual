using AutoFixture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMan.DataAccess.Repositories;
using UMan.DataAccess.Repositories.Exceptions;
using Xunit;

namespace UMan.DataAccess.Tests.Repositories
{
    public class PapersRepositoryTests
    {
        private readonly PapersRepository _papersRepository;

        public PapersRepositoryTests()
        {
            var fixtureDb = new StandartFixture();

            _papersRepository = new PapersRepository(fixtureDb.Context, fixtureDb.Mapper);
        }

        [Fact]
        public async void GetPaper_ShouldReturnsPaper()
        {
            //arrange
            var fixture = new Fixture().Build<Core.Paper>()
                .Without(p => p.Id)
                .Without(p => p.Articles)
                .Without(p => p.Author);

            Core.Paper factPaper = fixture.Create();

            int factPaperId = await _papersRepository.Add(factPaper);
            
            //act
            Core.Paper expectedPaper = await _papersRepository.Get(factPaperId);

            //assert
            Assert.Equal(factPaperId, expectedPaper.Id);

            Assert.Equal(expectedPaper.Created, factPaper.Created);
        }

        [Fact]
        public async void GetPapers_ShouldReturnsPapers()
        {
            //arrange
            var fixture = new Fixture().Build<Core.Paper>()
                .Without(p => p.Id)
                .Without(p => p.Articles)
                .Without(p => p.Author);

            IEnumerable<Core.Paper> factPapers = fixture.CreateMany(3);

            foreach(Core.Paper paper in factPapers)
            {
                await _papersRepository.Add(paper);
            }

            //act
            Core.Paper[] expectedAuthors = await _papersRepository.Get();

            //assert
            Assert.Equal(factPapers.Count(), expectedAuthors.Count());
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public async void GetPaper_ShouldThrowException(int id)
        {
            //arrange
            var fixture = new Fixture().Build<Core.Paper>()
                .With(p => p.Id, id)
                .Without(p => p.Articles)
                .Without(p => p.Author);

            Core.Paper factPaper = fixture.Create();
            //act
            Task<Core.Paper> get = _papersRepository.Get(id);
            //assert
            await Assert.ThrowsAsync<EntityNotFoundException<int>>(() => get);
        }

        [Fact]
        public async void DeletetePaper_ShouldReturnsTrue()
        {
            //arrange
            var fixture = new Fixture().Build<Core.Paper>()
                .Without(p => p.Id)
                .Without(p => p.Articles)
                .Without(p => p.Author);

            Core.Paper factPaper = fixture.Create();

            int paperId = await _papersRepository.Add(factPaper);
            
            //act
            bool isDeleted = await _papersRepository.Delete(paperId);

            //assert
            Assert.True(isDeleted);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public async void DeletePaper_ShouldReturnsTrue(int id)
        {
            Task<bool> deletePaper = _papersRepository.Delete(id);

            await Assert.ThrowsAsync<EntityNotFoundException<int>>(() => deletePaper);
        }

        [Fact]
        public async void UpdatePaper_ShouldReturnsPapersId()
        {
            //arrange
            var articleFixture = new Fixture().Build<Core.Article>();

            var paperFixture = new Fixture().Build<Core.Paper>()
                .With(p => p.Id, 1)
                .With(p => p.Articles, articleFixture.CreateMany(3).ToList())
                .Without(p => p.Author);

            Core.Paper factPaper = paperFixture.Create();

            await _papersRepository.Add(factPaper);

            Core.Paper updatedPaper = paperFixture
                .With(p => p.Articles, articleFixture.CreateMany(3).ToList())
                .Create();

            int expectedPaperId = await _papersRepository.Update(1, updatedPaper);

            Assert.Equal(factPaper.Id, expectedPaperId);

            Assert.NotEqual(factPaper, updatedPaper);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public async void UpdatePaper_ShouldReturnsException(int id)
        {
            //arrange
            var articleFixture = new Fixture().Build<Core.Article>();

            var paperFixture = new Fixture().Build<Core.Paper>()
                .With(p => p.Id, 1)
                .With(p => p.Articles, articleFixture.CreateMany(3).ToList())
                .Without(p => p.Author);

            Core.Paper factPaper = paperFixture.Create();

            await _papersRepository.Add(factPaper);

            Core.Paper updatedPaper = paperFixture
                .With(p => p.Articles, articleFixture.CreateMany(3).ToList())
                .Create();

            //act
            Task<int> update = _papersRepository.Update(id, updatedPaper);

            await Assert.ThrowsAnyAsync<EntityNotFoundException<int>>(() => update);
        }
    }
}
