using Microsoft.AspNetCore.Mvc;
using Moq;
using MovieAPI.Controllers.interfaces;
using MovieAPI.Controllers;
using MovieAPI.Dto;
using MovieAPI.Exceptions;
using MovieAPI.Models;
using MovieAPI.Service.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teste.Helpers;
using MovieAPI.Constants;

namespace Teste.UnitTests
{
    public class TestController
    {

        Mock<ICommandService> _command;
        Mock<IQueryService> _query;
        ControllerAPI _controller;

        public TestController()
        {
            _command = new Mock<ICommandService>();
            _query = new Mock<IQueryService>();
            _controller = new ControllerMovie(_query.Object, _command.Object);
        }

        [Fact]
        public async Task GetAll_ItemsDoNotExist()
        {
            _query.Setup(repo => repo.GetAll()).ThrowsAsync(new ItemsDoNotExist(Constants.ItemsDoNotExist));
            var result = await _controller.GetAll();

            var notFound = Assert.IsType<NotFoundObjectResult>(result.Result);

            Assert.Equal(404, notFound.StatusCode);
            Assert.Equal(Constants.ItemsDoNotExist, notFound.Value);

        }

        [Fact]
        public async Task GetAll_ValidData()
        {
            var movies = TestMovieFactory.CreateMovies(5);

            _query.Setup(repo => repo.GetAll()).ReturnsAsync(movies);

            var result = await _controller.GetAll();

            var okresult = Assert.IsType<OkObjectResult>(result.Result);

            var moviesAll = Assert.IsType<List<Movie>>(okresult.Value);

            Assert.Equal(5, moviesAll.Count);
            Assert.Equal(200, okresult.StatusCode);

        }

        [Fact]
        public async Task Create_InvalidData()
        {
            var create = new CreateRequest
            {
                Title = "test",
                Date = 0,
                Gender = "test"
            };

            _command.Setup(repo => repo.Create(It.IsAny<CreateRequest>())).ThrowsAsync(new InvalidDate(Constants.InvalidDate));

            var result = await _controller.CreateMovie(create);

            var bad = Assert.IsType<BadRequestObjectResult>(result.Result);

            Assert.Equal(400, bad.StatusCode);
            Assert.Equal(Constants.InvalidDate, bad.Value);

        }

        [Fact]
        public async Task Create_ValidData()
        {
            var create = new CreateRequest
            {
                Title = "test",
                Date = 2000,
                Gender = "test"
            };
            var movie = TestMovieFactory.CreateMovie(5);
            movie.Title = create.Title;
            movie.date = create.Date;
            movie.Gender = create.Gender;

            _command.Setup(repo => repo.Create(create)).ReturnsAsync(movie);

            var result = await _controller.CreateMovie(create);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);

            Assert.Equal(okResult.StatusCode, 200);
            Assert.Equal(movie, okResult.Value);

        }

        [Fact]
        public async Task Update_InvalidDate()
        {
            var update = new UpdateRequest
            {
                Date = 0
            };

            var movie = TestMovieFactory.CreateMovie(5);
            movie.date = update.Date.Value;

            _command.Setup(repo => repo.Update(5, update)).ThrowsAsync(new InvalidDate(Constants.InvalidDate));

            var result = await _controller.UpdateMovie(5, update);

            var bad = Assert.IsType<BadRequestObjectResult>(result.Result);

            Assert.Equal(bad.StatusCode, 400);
            Assert.Equal(bad.Value, Constants.InvalidDate);


        }

        [Fact]
        public async Task Update_ValidData()
        {
            var update = new UpdateRequest
            {
                Date = 200
            };
            var movie = TestMovieFactory.CreateMovie(5);
            movie.date = update.Date.Value;

            _command.Setup(repo => repo.Update(5, update)).ReturnsAsync(movie);

            var result = await _controller.UpdateMovie(5, update);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);

            Assert.Equal(okResult.StatusCode, 200);
            Assert.Equal(okResult.Value, movie);

        }

        [Fact]
        public async Task Delete_ItemDoesNotExist()
        {
            _command.Setup(repo => repo.Delete(1)).ThrowsAsync(new ItemDoesNotExist(Constants.ItemDoesNotExist));

            var result = await _controller.DeleteMovie(1);

            var notFound = Assert.IsType<NotFoundObjectResult>(result.Result);

            Assert.Equal(notFound.StatusCode, 404);
            Assert.Equal(notFound.Value, Constants.ItemDoesNotExist);

        }

        [Fact]
        public async Task Delete_ValidData()
        {
            var movie = TestMovieFactory.CreateMovie(1);

            _command.Setup(repo => repo.Delete(1)).ReturnsAsync(movie);

            var result = await _controller.DeleteMovie(1);

            var okReult = Assert.IsType<OkObjectResult>(result.Result);

            Assert.Equal(200, okReult.StatusCode);
            Assert.Equal(movie, okReult.Value);

        }

    }
}
