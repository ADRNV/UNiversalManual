﻿using AutoFixture;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMan.DataAccess.Repositories;
using Xunit;
using UMan.DataAccess.Repositories.Exceptions;
using Moq;
using UMan.Core.Repositories;

namespace UMan.DataAccess.Tests.Repositories
{
    
    public class AuthorsRepositoryTests : IClassFixture<StandartFixture>
    {
        private readonly AuthorsRepository _authorsRepository;

        private readonly Mock<IRepository<Core.Author>> _authorsRepositoryMock;

        private readonly IMapper _mapper;

        public AuthorsRepositoryTests()
        {
            var fixtureDb = new StandartFixture();

            _authorsRepository = new AuthorsRepository(fixtureDb.Context, fixtureDb.Mapper);
        }

        [Fact]
        public async void GetAuthor_ShouldReturnsAuthor()
        {
            //arrange 
            var fixture = new Fixture().Build<Core.Author>()
                .With(a => a.Id, 1)
                .Without(a => a.Papers);

            Core.Author factAuthor = fixture.Create();

            factAuthor.Papers = new List<Core.Paper>() { new Core.Paper()};

            int authorId = await _authorsRepository.Add(factAuthor);

            //act
            Core.Author expectedAutor = await _authorsRepository.Get(factAuthor.Id);

            //assert
            Assert.Equal(factAuthor.Id, expectedAutor.Id);

            Assert.True(factAuthor.Email == expectedAutor.Email);

            Assert.True(factAuthor.Name == expectedAutor.Name);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public async void GetAuthor_ShouldThrowException(int id)
        {
            //arrange 
            var authorFixture = new Fixture().Build<Core.Author>()
                .With(a => a.Id, id)
                .Without(a => a.Papers);

            Core.Author factAuthor = authorFixture.Create();

            Task<Core.Author> get = _authorsRepository.Get(id);

            await Assert.ThrowsAnyAsync<EntityNotFoundException<int>>(() => get);   
        }

        [Fact]
        public async void GetAuthors_ShouldReturnsAuthorsArray()
        {
            //arrange
            var fixture = new Fixture().Build<Core.Author>()
               .With(a => a.Id, 0)
               .Without(a => a.Papers);

            IEnumerable<Core.Author> factAuthors = fixture.CreateMany(3)
                .ToArray();

            foreach(var author in factAuthors)
            {
                await _authorsRepository.Add(author);
            }

            //act
            Core.Author[] expectedAuthors = await _authorsRepository.Get();

            //assert
            Assert.Equal(factAuthors.Count(), expectedAuthors.Count());
        }

        [Fact]
        public async void DeleteAuthor_ShouldReturnsTrue()
        {
            //arrange 
            var fixture = new Fixture().Build<Core.Author>()
                .With(a => a.Id, 0)
                .Without(a => a.Papers);

            Core.Author factAuthor = fixture.Create();

            int authorId = await _authorsRepository.Add(factAuthor);
            //act
            bool isDeleted = await _authorsRepository.Delete(authorId);

            Assert.True(isDeleted);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public async void DeleteAuthor_ShouldThrowsException(int id)
        {

            Task<bool> deleteAuthor = _authorsRepository.Delete(id);

            await Assert.ThrowsAsync<EntityNotFoundException<int>>(() => deleteAuthor);
        }

        [Fact]
        public async void UpdateAuthor_ShouldReturnsAuthorId()
        {
            //arrange 
            var fixture = new Fixture().Build<Core.Author>()
                .With(a => a.Id, 0)
                .Without(a => a.Papers);

            Core.Author factAuthor = fixture.Create();

            await _authorsRepository.Add(factAuthor);

            //act
            factAuthor.Name = Guid.NewGuid().ToString();

            int authorId = await _authorsRepository.Update(1, factAuthor);

            Assert.Equal(1, authorId);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public async void UpdateAuthor_ShouldThorowException(int id)
        {
            //arrange 
            var fixture = new Fixture().Build<Core.Author>()
                .With(a => a.Id, 0)
                .Without(a => a.Papers);

            Core.Author factAuthor = fixture.Create();

            await _authorsRepository.Add(factAuthor);

            //assert
            await Assert.ThrowsAsync<EntityNotFoundException<int>>(async () =>
            {
                //act
                await _authorsRepository.Update(id, factAuthor);
            });
        }
    }
}