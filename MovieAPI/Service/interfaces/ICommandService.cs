using Microsoft.Extensions.Logging;
using MovieAPI.Dto;
using MovieAPI.Models;

namespace MovieAPI.Service.interfaces
{
    public interface ICommandService
    {
        Task<Movie> Create(CreateRequest request);

        Task<Movie> Update(int id, UpdateRequest request);

        Task<Movie> Delete(int id);
    }
}
