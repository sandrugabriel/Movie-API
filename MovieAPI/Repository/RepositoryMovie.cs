using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieAPI.Data;
using MovieAPI.Dto;
using MovieAPI.Models;
using MovieAPI.Repository.interfaces;
using System;

namespace MovieAPI.Repository
{
    public class RepositoryMovie : IRepository
    {

        private AppDbContext _context;
        private IMapper _mapper;

        public RepositoryMovie(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Movie>> GetAllAsync()
        {
            return await _context.Movie.ToListAsync();
        }

        public async Task<Movie> GetByIdAsync(int id)
        {
            List<Movie> all = await _context.Movie.ToListAsync();

            for (int i = 0; i < all.Count; i++)
            {
                if (all[i].Id == id) return all[i];
            }

            return null;
        }

        public async Task<Movie> GetByNameAsync(string name)
        {
            List<Movie> all = await _context.Movie.ToListAsync();

            for (int i = 0; i < all.Count; i++)
            {
                if (all[i].Title.Equals(name))
                {
                    return all[i];
                }
            }

            return null;
        }


        public async Task<Movie> Create(CreateRequest request)
        {

            var movie = _mapper.Map<Movie>(request);

            _context.Movie.Add(movie);

            await _context.SaveChangesAsync();

            return movie;

        }

        public async Task<Movie> Update(int id, UpdateRequest request)
        {

            var movie = await _context.Movie.FindAsync(id);

            movie.Title = request.Title ?? movie.Title;
            movie.Gender = request.Gender ?? movie.Gender;
            movie.date = request.Date ?? movie.date;

            _context.Movie.Update(movie);

            await _context.SaveChangesAsync();

            return movie;

        }

        public async Task<Movie> DeleteById(int id)
        {
            var movie = await _context.Movie.FindAsync(id);

            _context.Movie.Remove(movie);

            await _context.SaveChangesAsync();

            return movie;
        }


    }
}
