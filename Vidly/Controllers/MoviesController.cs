using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Vidly.Models;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        public IActionResult Index()
        {

            var movies = GetMovies();

            return View(movies);
        }

        public IActionResult Details(int id)
        {
            var movies = GetMovies();

            var movie = movies.SingleOrDefault(c => c.Id == id);

            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        private IEnumerable<Movie> GetMovies()
        {
            var movies = new List<Movie>()
            {
                new Movie(){Id = 1, Name = "Shrek"},
                new Movie(){Id = 2, Name = "Wall-e"},
            };

            return movies;
        }
    }
}
