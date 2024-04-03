using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;
using Microsoft.Extensions.Logging;
using MovieAPI.Dto;
using MovieAPI.Models;

namespace MovieAPI.Controllers.interfaces
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public abstract class ControllerAPI : ControllerBase
    {


        [HttpGet("/all")]
        [ProducesResponseType(statusCode: 200, type: typeof(List<Movie>))]
        [ProducesResponseType(statusCode: 400, type: typeof(String))]
        public abstract Task<ActionResult<List<Movie>>> GetAll();

        [HttpGet("/findById")]
        [ProducesResponseType(statusCode: 200, type: typeof(Movie))]
        [ProducesResponseType(statusCode: 400, type: typeof(String))]
        public abstract Task<ActionResult<Movie>> GetById([FromQuery] int id);

        [HttpGet("/findByName")]
        [ProducesResponseType(statusCode: 200, type: typeof(Movie))]
        [ProducesResponseType(statusCode: 400, type: typeof(String))]
        public abstract Task<ActionResult<Movie>> GetByName([FromQuery] string name);

        [HttpPost("/createMovie")]
        [ProducesResponseType(statusCode: 201, type: typeof(Movie))]
        [ProducesResponseType(statusCode: 400, type: typeof(String))]
        public abstract Task<ActionResult<Movie>> CreateMovie(CreateRequest request);

        [HttpPut("/updateMovie")]
        [ProducesResponseType(statusCode: 200, type: typeof(Movie))]
        [ProducesResponseType(statusCode: 400, type: typeof(String))]
        [ProducesResponseType(statusCode: 404, type: typeof(String))]
        public abstract Task<ActionResult<Movie>> UpdateMovie([FromQuery] int id, UpdateRequest request);

        [HttpDelete("/deleteMovie")]
        [ProducesResponseType(statusCode: 200, type: typeof(Movie))]
        [ProducesResponseType(statusCode: 404, type: typeof(String))]
        public abstract Task<ActionResult<Movie>> DeleteMovie([FromQuery] int id);


    }
}
