using Microsoft.Extensions.Logging;
using MovieAPI.Dto;
using MovieAPI.Exceptions;
using MovieAPI.Models;
using MovieAPI.Repository.interfaces;
using MovieAPI.Service.interfaces;
using System.ComponentModel.Design;

namespace MovieAPI.Service
{
    public class CommandService : ICommandService
    {


        private IRepository _repository;

        public CommandService(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<Movie> Create(CreateRequest request)
        {

            if (request.Date <= 1000)
            {
                throw new InvalidDate(Constants.Constants.InvalidDate);
            }

            var movie = await _repository.Create(request);

            return movie;
        }

        public async Task<Movie> Update(int id, UpdateRequest request)
        {

            var movie = await _repository.GetByIdAsync(id);
            if (movie == null)
            {
                throw new ItemDoesNotExist(Constants.Constants.ItemDoesNotExist);
            }


            if (movie.date <= 1000)
            {
                throw new InvalidDate(Constants.Constants.InvalidDate);
            }
            movie = await _repository.Update(id, request);
            return movie;
        }

        public async Task<Movie> Delete(int id)
        {

            var movie = await _repository.GetByIdAsync(id);
            if (movie == null)
            {
                throw new ItemDoesNotExist(Constants.Constants.ItemDoesNotExist);
            }
            await _repository.DeleteById(id);
            return movie;
        }
    }
}
