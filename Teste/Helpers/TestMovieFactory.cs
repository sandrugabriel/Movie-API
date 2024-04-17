using MovieAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teste.Helpers
{
    public class TestMovieFactory
    {

        public static Movie CreateMovie(int id)
        {
            return new Movie
            {
                Id = id,
                date = 2000 + id,
                Gender = "test" + id,
                Title = "test" + id
            };
        }

        public static List<Movie> CreateMovies(int count)
        {
            List<Movie> movies = new List<Movie>();
            for (int i = 1; i <= count; i++)
            {
                movies.Add(CreateMovie(i));
            }

            return movies;
        }

    }
}
