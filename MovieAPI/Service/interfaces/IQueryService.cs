using Microsoft.Extensions.Logging;
using MovieAPI.Models;

namespace MovieAPI.Service.interfaces
{
    public interface IQueryService
    {
        Task<List<Movie>> GetAll();
        Task<Movie> GetById(int id);

        Task<Movie> GetByNameAsync(string name);
    }
}
