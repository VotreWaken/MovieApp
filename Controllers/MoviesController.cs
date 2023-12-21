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
        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await db.Movie
                .Where(m => m.Id == id.Value)
                .FirstOrDefaultAsync();

            if (movie == null)
            {
                return NotFound();
            }

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

            return View(movie);
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

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Получите список всех жанров
            ViewBag.AllGenres = await db.Genre.ToListAsync();

            var student = await db.Movie.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }


            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MovieTitle,MovieDescription,FilmImage,Director,ProductionDate,SelectedGenres")] Movie movie)
        {
            if (id != movie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var selectedGenres = movie.SelectedGenres;

                    // Очистите существующие связи жанра с фильмом
                    db.MovieGenre.RemoveRange(db.MovieGenre.Where(mg => mg.MovieId == id));

                    // Добавьте новые связи жанра
                    foreach (var genreId in selectedGenres)
                    {
                        db.MovieGenre.Add(new MovieGenre { MovieId = id, GenreId = genreId });
                    }

                    db.Update(movie);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(movie.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            ViewBag.AllGenres = await db.Genre.ToListAsync();
            return View(movie);
        }
        private bool StudentExists(int id)
        {
            return db.Movie.Any(e => e.Id == id);
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
