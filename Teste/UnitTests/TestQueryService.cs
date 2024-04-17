using Moq;
using MovieAPI.Exceptions;
using MovieAPI.Models;
using MovieAPI.Repository.interfaces;
using MovieAPI.Service.interfaces;
using MovieAPI.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teste.Helpers;
using MovieAPI.Constants;

namespace Teste.UnitTests
{
    public class TestQueryService
    {

        Mock<IRepository> _mock;
        IQueryService _service;

        public TestQueryService()
        {
            _mock = new Mock<IRepository>();
            _service = new QueryService(_mock.Object);
        }

        [Fact]
        public async Task GetAll_ItemsDoNotExist()
        {
            _mock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<Movie>());

            var exception = await Assert.ThrowsAsync<ItemsDoNotExist>(() => _service.GetAll());

            Assert.Equal(exception.Message, Constants.ItemsDoNotExist);

        }

        [Fact]
        public async Task GetAll_ReturnAllMovie()
        {
            var movies = TestMovieFactory.CreateMovies(5);

            _mock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(movies);

            var result = await _service.GetAll();

            Assert.NotNull(result);
            Assert.Contains(movies[1], result);

        }

        [Fact]
        public async Task GetById_ItemDoesNotExist()
        {
            _mock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((Movie)null);

            var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _service.GetById(1));

            Assert.Equal(Constants.ItemDoesNotExist, exception.Message);
        }

        [Fact]
        public async Task GetById_ReturnMovie()
        {

            var movie = TestMovieFactory.CreateMovie(5);

            _mock.Setup(repo => repo.GetByIdAsync(5)).ReturnsAsync(movie);

            var result = await _service.GetById(5);

            Assert.NotNull(result);
            Assert.Equal(movie, result);

        }

        [Fact]
        public async Task GetByName_ItemDoesNotExist()
        {

            _mock.Setup(repo => repo.GetByNameAsync("")).ReturnsAsync((Movie)null);

            var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _service.GetByNameAsync(""));

            Assert.Equal(Constants.ItemDoesNotExist, exception.Message);
        }

        [Fact]
        public async Task GetByName_ReturnMovie()
        {
            var movie = TestMovieFactory.CreateMovie(3);

            movie.Title = "test";
            _mock.Setup(repo => repo.GetByNameAsync("test")).ReturnsAsync(movie);

            var result = await _service.GetByNameAsync("test");

            Assert.NotNull(result);
            Assert.Equal(movie, result);
        }

    }
}
