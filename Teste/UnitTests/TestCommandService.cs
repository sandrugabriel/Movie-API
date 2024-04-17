using Moq;
using MovieAPI.Constants;
using MovieAPI.Dto;
using MovieAPI.Exceptions;
using MovieAPI.Models;
using MovieAPI.Repository.interfaces;
using MovieAPI.Service;
using MovieAPI.Service.interfaces;
using Teste.Helpers;

namespace Teste.UnitTests
{
    public class TestCommandService
    {

        Mock<IRepository> _mock;
        ICommandService _service;

        public TestCommandService()
        {
            _mock = new Mock<IRepository>();
            _service = new CommandService(_mock.Object);
        }

        [Fact]
        public async Task Create_InvalidData()
        {

            var create = new CreateRequest
            {
                Title = "Test",
                Date = 0,
                Gender = "test"
            };

            _mock.Setup(repo => repo.Create(create)).ReturnsAsync((Movie)null);

            var exception = await Assert.ThrowsAsync<InvalidDate>(() => _service.Create(create));

            Assert.Equal(Constants.InvalidDate, exception.Message);


        }

        [Fact]
        public async Task Create_ReturnMovie()
        {
            var create = new CreateRequest
            {
                Title = "Test",
                Date = 2000,
                Gender = "test"
            };

            var movie = TestMovieFactory.CreateMovie(5);
            movie.Title = create.Title;
            movie.date = create.Date;
            movie.Gender = create.Gender;
            _mock.Setup(repo => repo.Create(It.IsAny<CreateRequest>())).ReturnsAsync(movie);

            var result = await _service.Create(create);

            Assert.NotNull(result);

            Assert.Equal(result, movie);

        }

        [Fact]
        public async Task Update_ItemDoesNotExist()
        {
            var update = new UpdateRequest
            {
                Date = 2000
            };

            _mock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((Movie)null);

            var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _service.Update(1, update));

            Assert.Equal(Constants.ItemDoesNotExist, exception.Message);

        }

        [Fact]
        public async Task Update_InvalidData()
        {
            var update = new UpdateRequest
            {
                Date = 0
            };

            var movie = TestMovieFactory.CreateMovie(5);
            movie.date = update.Date.Value;


            _mock.Setup(repo => repo.GetByIdAsync(5)).ReturnsAsync(movie);

            var exception = await Assert.ThrowsAsync<InvalidDate>(() => _service.Update(5, update));

            Assert.Equal(Constants.InvalidDate, exception.Message);
        }

        [Fact]
        public async Task Update_ValidData()
        {
            var update = new UpdateRequest
            {
                Date = 2010
            };

            var movie = TestMovieFactory.CreateMovie(5);
            movie.date = update.Date.Value;

            _mock.Setup(repo => repo.GetByIdAsync(5)).ReturnsAsync(movie);
            _mock.Setup(repo => repo.Update(It.IsAny<int>(), It.IsAny<UpdateRequest>())).ReturnsAsync(movie);

            var result = await _service.Update(5, update);

            Assert.NotNull(result);
            Assert.Equal(movie, result);

        }

        [Fact]
        public async Task Delete_ItemDoesNotExist()
        {
            _mock.Setup(repo => repo.DeleteById(It.IsAny<int>())).ReturnsAsync((Movie)null);

            var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _service.Delete(5));

            Assert.Equal(exception.Message, Constants.ItemDoesNotExist);
        }

        [Fact]
        public async Task Delete_ValidData()
        {
            var movie = TestMovieFactory.CreateMovie(1);

            _mock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(movie);

            var result = await _service.Delete(1);

            Assert.NotNull(result);
            Assert.Equal(movie, result);
        }

    }
}