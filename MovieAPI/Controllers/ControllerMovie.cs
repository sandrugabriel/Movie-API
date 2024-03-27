using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using MovieAPI.Dto;
using MovieAPI.Models;
using MovieAPI.Repository.interfaces;
using System;

namespace MovieAPI.Controllers
{
    [ApiController]
    [Route("api/v1/movie")]
    public class ControllerMovie : ControllerBase
    {

        private readonly ILogger<ControllerMovie> _logger;

        private IRepository _repository;

        public ControllerMovie(ILogger<ControllerMovie> logger, IRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<Movie>>> GetAll()
        {
            var products = await _repository.GetAllAsync();
            return Ok(products);
        }


        [HttpGet("/findById")]
        public async Task<ActionResult<Movie>> GetById([FromQuery] int id)
        {
            var movie = await _repository.GetByIdAsync(id);
            return Ok(movie);
        }


        [HttpGet("/find/{name}")]
        public async Task<ActionResult<Movie>> GetByNameRoute([FromRoute] string name)
        {
            var movie = await _repository.GetByNameAsync(name);
            return Ok(movie);
        }


        [HttpPost("/create")]
        public async Task<ActionResult<Movie>> Create([FromBody] CreateRequest request)
        {
            var movie = await _repository.Create(request);
            return Ok(movie);

        }

        [HttpPut("/update")]
        public async Task<ActionResult<Movie>> Update([FromQuery] int id, [FromBody] UpdateRequest request)
        {
            var movie = await _repository.Update(id, request);
            return Ok(movie);
        }

        [HttpDelete("/deleteById")]
        public async Task<ActionResult<Movie>> DeleteCarById([FromQuery] int id)
        {
            var movie = await _repository.DeleteById(id);
            return Ok(movie);
        }


    }
}
