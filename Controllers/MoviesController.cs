using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApplication.Models;

namespace MovieApplication.Controllers
{
    public class MoviesController : Controller
    {
        private readonly ILogger<MoviesController> _logger;

        MovieContext db;
        public MoviesController(MovieContext context)
        {
            db = context;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Movie> movies = await db.Movie
                .OrderBy(m => m.Id)
                .ToListAsync();

            foreach (var movie in movies)
            {
                var genreIds = await db.MovieGenre
                    .Where(mg => mg.MovieId == movie.Id)
                    .Select(mg => mg.GenreId)
                    .ToListAsync();

                var genres = await db.Genre
                    .Where(g => genreIds.Contains(g.Id))
                    .ToListAsync();

                var movieGenres = genres.Select(g => new MovieGenre
                {
                    Genre = g,
                    Movie = movie
                }).ToList();

                movie.MovieGenres = movieGenres;
            }

            ViewBag.Movie = movies;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
