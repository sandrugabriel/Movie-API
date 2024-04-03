using Microsoft.Extensions.Logging;
using MovieAPI.Exceptions;
using MovieAPI.Models;
using MovieAPI.Repository.interfaces;
using MovieAPI.Service.interfaces;

namespace MovieAPI.Service
{
    public class QueryService : IQueryService
    {

        private IRepository _repository;

        public QueryService(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Movie>> GetAll()
        {
            var movie = await _repository.GetAllAsync();

            if (movie.Count() == 0)
            {
                throw new ItemsDoNotExist(Constants.Constants.ItemsDoNotExist);
            }

            return (List<Movie>)movie;
        }

        public async Task<Movie> GetByNameAsync(string name)
        {
            var movie = await _repository.GetByNameAsync(name);

            if (movie == null)
            {
                throw new ItemDoesNotExist(Constants.Constants.ItemDoesNotExist);
            }

            return movie;
        }

        public async Task<Movie> GetById(int id)
        {
            var movie = await _repository.GetByIdAsync(id);

            if (movie == null)
            {
                throw new ItemDoesNotExist(Constants.Constants.ItemDoesNotExist);
            }

            return movie;
        }
    }
}
