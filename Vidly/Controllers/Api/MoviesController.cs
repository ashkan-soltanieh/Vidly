using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vidly.Domain;
using Vidly.Dtos;
using Vidly.Persistence;

namespace Vidly.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly VidlyDbContext _context;
        private readonly IMapper _mapper;

        public MoviesController(VidlyDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("/api/movies")]
        public async Task<IActionResult> GetMovies()
        {
            var movies = 
                (await _context.Movies
                    .Include(m=> m.Genre)
                    .ToListAsync())
                .Select(_mapper.Map<Movie, MovieDto>);

            return Ok(movies);
        }

        [HttpGet("/api/movies/{id}")]
        public async  Task<IActionResult> GetMovie(int id)
        {
            var movie = await _context.Movies.SingleOrDefaultAsync(m => m.Id == id);
            
            if (movie == null)
                return NotFound();
            
            return Ok(_mapper.Map<Movie, MovieDto>(movie));
        }

        [HttpPost("/api/movies")]
        public async Task<IActionResult> CreateMovie(MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var movie = _mapper.Map<MovieDto, Movie>(movieDto);

            movie.DateAdded = DateTime.Today;

            await _context.Movies.AddAsync(movie);
            await _context.SaveChangesAsync();

            movieDto.Id = movie.Id;

            return Created(new Uri(Request.GetDisplayUrl() + movieDto.Id), movieDto);
        }

        [HttpPut("/api/movies/{id}")]
        public async Task<IActionResult> UpdateMovie(int id, MovieDto movieDto)
        {
            var movieInDb = await _context.Movies.SingleOrDefaultAsync(m => m.Id == id);

            if (movieInDb == null)
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();
            
            _mapper.Map<MovieDto, Movie>(movieDto, movieInDb);

            await _context.SaveChangesAsync();

            return Ok(_mapper.Map<Movie, MovieDto>(movieInDb));
        }
        
        [HttpDelete("/api/movies/{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var movieInDb = await _context.Movies.SingleOrDefaultAsync(m => m.Id == id);

            if (movieInDb == null)
                return NotFound();

            _context.Movies.Remove(movieInDb);
            await _context.SaveChangesAsync();

            return Ok(_mapper.Map<Movie, MovieDto>(movieInDb));
        }
    }
}
