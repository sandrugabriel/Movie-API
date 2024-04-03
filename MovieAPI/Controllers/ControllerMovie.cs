using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using MovieAPI.Controllers.interfaces;
using MovieAPI.Dto;
using MovieAPI.Exceptions;
using MovieAPI.Models;
using MovieAPI.Repository.interfaces;
using MovieAPI.Service.interfaces;
using System;

namespace MovieAPI.Controllers
{
    public class ControllerMovie : ControllerAPI
    {

        private IQueryService _queryService;
        private ICommandService _commandService;

        public ControllerMovie(IQueryService queryService, ICommandService commandService)
        {
            _queryService = queryService;
            _commandService = commandService;
        }

        public override async Task<ActionResult<List<Movie>>> GetAll()
        {
            try
            {
                var eventss = await _queryService.GetAll();

                return Ok(eventss);

            }
            catch (ItemsDoNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

        public override async Task<ActionResult<Movie>> GetByName([FromQuery] string name)
        {

            try
            {
                var events = await _queryService.GetByNameAsync(name);
                return Ok(events);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }

        }

        public override async Task<ActionResult<Movie>> GetById([FromQuery] int id)
        {

            try
            {
                var events = await _queryService.GetById(id);
                return Ok(events);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }

        }

        public override async Task<ActionResult<Movie>> CreateMovie(CreateRequest request)
        {
            try
            {
                var events = await _commandService.Create(request);
                return Ok(events);
            }
            catch (InvalidDate ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public override async Task<ActionResult<Movie>> UpdateMovie([FromQuery] int id, UpdateRequest request)
        {
            try
            {
                var events = await _commandService.Update(id, request);
                return Ok(events);
            }
            catch (InvalidDate ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

        public override async Task<ActionResult<Movie>> DeleteMovie([FromQuery] int id)
        {
            try
            {
                var events = await _commandService.Delete(id);
                return Ok(events);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }


    }
}
