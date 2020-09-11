using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vidly.Domain;
using Vidly.Models;
using Vidly.Persistence;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        private readonly VidlyDbContext _context;

        public MoviesController(VidlyDbContext context)
        {
            _context = context;
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Details(int id)
        {
            var movie = await _context.Movies
                .Include(m=>m.Genre)
                .SingleOrDefaultAsync(c => c.Id == id);

            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }


        public async Task<IActionResult> New()
        {
            var genres = await _context.Genres.ToListAsync();

            var viewModel = new MovieFormViewModel()
            {
                Genres = genres
            };
            return View("MovieForm" ,viewModel);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var movie = await _context.Movies.
                SingleOrDefaultAsync(m => m.Id == id);
            
            if (movie == null)
                return NotFound();
            
            var genres = await _context.Genres.ToListAsync();
            
            var viewModel = new MovieFormViewModel(movie)
            {
                Genres = genres
            };

            return View("MovieForm", viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Save(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new MovieFormViewModel(movie)
                {
                    Genres = await _context.Genres.ToListAsync()
                };
                return View("MovieForm",viewModel);
            }

            if (movie.Id == 0)
            {
                movie.DateAdded = DateTime.Now;
                await _context.Movies.AddAsync(movie);
            }
            else
            {
                var movieInDb = await _context.Movies.
                    SingleAsync(m => m.Id == movie.Id);
                movieInDb.Name = movie.Name;
                movieInDb.ReleaseDate = movie.ReleaseDate;
                movieInDb.GenreId = movie.GenreId;
                movieInDb.NumberInStock = movie.NumberInStock;
            }

            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Movies");
        }
    }
}
