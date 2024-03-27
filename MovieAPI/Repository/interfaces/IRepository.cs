using MovieAPI.Models;
using System;

namespace MovieAPI.Repository.interfaces
{
    public interface IRepository
    {
        Task<IEnumerable<Movie>> GetAllAsync();

        Task<Movie> GetByNameAsync(string name);

        Task<Movie> GetByIdAsync(int id);
    }
}
