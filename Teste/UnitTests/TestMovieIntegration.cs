using MovieAPI.Dto;
using MovieAPI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Teste.Helpers;
using Teste.Infrastructure;

namespace Teste.UnitTests
{
    public class TestMovieIntegration : IClassFixture<ApiWebApplicationFactory>
    {

        private readonly HttpClient _client;

        public TestMovieIntegration(ApiWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetAllMovies_MoviesFound_ReturnsOkStatusCode_ValidResponse()
        {
            var createMovieRequest = TestMovieFactory.CreateMovie(1);
            var content = new StringContent(JsonConvert.SerializeObject(createMovieRequest), Encoding.UTF8, "application/json");
            await _client.PostAsync("/api/v1/ControllerMovie/CreateMovie", content);

            var response = await _client.GetAsync("/api/v1/ControllerMovie/All");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetMovieById_MovieFound_ReturnsOkStatusCode()
        {
            var createMovieRequest = new CreateRequest
            { Gender = "action", Title = "ASasdadd", Date = 2000 };
            var content = new StringContent(JsonConvert.SerializeObject(createMovieRequest), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/v1/ControllerMovie/createMovie", content);
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Movie>(responseString);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(result.Title, createMovieRequest.Title);
        }

        [Fact]
        public async Task GetMovieById_MovieNotFound_ReturnsNotFoundStatusCode()
        {
            var response = await _client.GetAsync("/api/v1/Movie/findById?id=9999");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Post_Create_ValidRequest_ReturnsCreatedStatusCode()
        {
            var request = "/api/v1/ControllerMovie/CreateMovie";
            var createMovieRequest = new CreateRequest
            { Gender = "action", Title = "ASasdadd", Date = 2000 };
            var content = new StringContent(JsonConvert.SerializeObject(createMovieRequest), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(request, content);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Movie>(responseString);

            Assert.NotNull(result);
            Assert.Equal(createMovieRequest.Title, result.Title);
        }

        [Fact]
        public async Task Put_Update_ValidRequest_ReturnsAcceptedStatusCode()
        {
            var request = "/api/v1/ControllerMovie/createMovie";
            var createMovie = new CreateRequest
            { Gender = "action", Title = "ASasdadd", Date = 2000 };
            var content = new StringContent(JsonConvert.SerializeObject(createMovie), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(request, content);
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Movie>(responseString);

            request = $"/api/v1/ControllerMovie/UpdateMovie?id={result.Id}";
            var updateMovie = new UpdateRequest { Title = "12test" };
            content = new StringContent(JsonConvert.SerializeObject(updateMovie), Encoding.UTF8, "application/json");

            response = await _client.PutAsync(request, content);
            var responceStringUp = await response.Content.ReadAsStringAsync();
            var result1 = JsonConvert.DeserializeObject<Movie>(responceStringUp);


            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(result1.Title, updateMovie.Title);
        }

        [Fact]
        public async Task Put_Update_InvalidMovieDate_ReturnsBadRequestStatusCode()
        {
            var request = "/api/v1/ControllerMovie/CreateMovie";
            var createMovie = new CreateRequest
            { Gender = "action", Title = "ASasdadd", Date = 2000 };
            var content = new StringContent(JsonConvert.SerializeObject(createMovie), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(request, content);
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Movie>(responseString);

            request = $"/api/v1/ControllerMovie/UpdateMovie?id={result.Id}";
            var updateMovie = new UpdateRequest { Date = 0 };
            content = new StringContent(JsonConvert.SerializeObject(updateMovie), Encoding.UTF8, "application/json");

            response = await _client.PutAsync(request, content);
            var responceStringUp = await response.Content.ReadAsStringAsync();
            var result1 = JsonConvert.DeserializeObject<Movie>(responseString);


            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.NotEqual(result1.date, updateMovie.Date);
        }

        [Fact]
        public async Task Put_Update_MovieDoesNotExist_ReturnsNotFoundStatusCode()
        {
            var request = "/api/v1/ControllerMovie/UpdateMovie";
            var updateMovie = new UpdateRequest { Title = "asd" };
            var content = new StringContent(JsonConvert.SerializeObject(updateMovie), Encoding.UTF8, "application/json");

            var response = await _client.PutAsync(request, content);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Delete_Delete_MovieExists_ReturnsDeletedMovie()
        {
            var request = "/api/v1/ControllerMovie/CreateMovie";
            var createMovie = new CreateRequest
            { Gender = "action", Title = "ASasdadd", Date = 2000 };
            var content = new StringContent(JsonConvert.SerializeObject(createMovie), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(request, content);
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Movie>(responseString)!;

            request = $"/api/v1/ControllerMovie/DeleteMovie?id={result.Id}";

            response = await _client.DeleteAsync(request);
            var responceString = await response.Content.ReadAsStringAsync();
            var result1 = JsonConvert.DeserializeObject<Movie>(responseString);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(result1.Title, createMovie.Title);
        }

        [Fact]
        public async Task Delete_Delete_MovieDoesNotExist_ReturnsNotFoundStatusCode()
        {
            var request = "/api/v1/ControllerMovie/DeleteMovie?id=7";

            var response = await _client.DeleteAsync(request);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
