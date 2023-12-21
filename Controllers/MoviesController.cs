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
        private readonly IWebHostEnvironment _appEnvironment;
        public MoviesController(MovieContext context, IWebHostEnvironment appEnvironment)
        {
            db = context;
            _appEnvironment = appEnvironment;
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MovieTitle,MovieDescription,FilmImage,Director,ProductionDate,SelectedGenres")] Movie movie, IFormFile fileUpload)
        {
            ViewBag.AllGenres = db.Genre.ToList();
            if (ModelState.IsValid && fileUpload.Length > 0)
            {
                if (fileUpload != null && fileUpload.Length > 0)
                {
                    //Check upload file extension
                    string ext = fileUpload.ContentType.ToLower();
                    if (ext != "image/jpg" &&
                        ext != "image/jpeg" &&
                        ext != "image/bmp" &&
                        ext != "image/pjpeg" &&
                        ext != "image/gif" &&
                        ext != "image/x-png" &&
                        ext != "image/png")
                    {
                        ModelState.AddModelError("", "Неверное расширение файла! Выберите другой файл.");
                        return View(movie);
                    }

                    string path = "/img/" + fileUpload.FileName;

                    using (FileStream filestream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                        await fileUpload.CopyToAsync(filestream);
                    movie.FilmImage = path;
                }
                db.Add(movie);
                await db.SaveChangesAsync(); // Сохраняем изменения, чтобы movie.Id получил свое значение

                var selectedGenres = movie.SelectedGenres;

                // Добавить связи жанра
                foreach (var genreId in selectedGenres)
                {
                    db.MovieGenre.Add(new MovieGenre { MovieId = movie.Id, GenreId = genreId });
                }

                await db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(movie);
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,MovieTitle,MovieDescription,FilmImage,Director,ProductionDate,SelectedGenres")] Movie movie, IFormFile? fileUpload)
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

                    if (fileUpload != null && fileUpload.Length > 0)
                    {
                        //Check upload file extension
                        string ext = fileUpload.ContentType.ToLower();
                        if (ext != "image/jpg" &&
                            ext != "image/jpeg" &&
                            ext != "image/bmp" &&
                            ext != "image/pjpeg" &&
                            ext != "image/gif" &&
                            ext != "image/x-png" &&
                            ext != "image/png")
                        {
                            ModelState.AddModelError("", "Неверное расширение файла! Выберите другой файл.");
                            return View(movie);
                        }

                        string path = "/img/" + fileUpload.FileName;

                        using (FileStream filestream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                            await fileUpload.CopyToAsync(filestream);
                        movie.FilmImage = path;
                    }
                    else
                    {
                        IQueryable<string> oldPath = from _movie in db.Movie
                                                     where _movie.Id == movie.Id
                                                     select _movie.FilmImage;
                        movie.FilmImage = oldPath.First();
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

        // Delete Method 
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            Movie? movie = await db.Movie
                .FirstOrDefaultAsync(m => m.Id == id);

            if (movie == null)
                return NotFound();

            return View(movie);
        }

        // POST: Movies/Delete/Id
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Находим фильм по id
            Movie? movie = await db.Movie.FindAsync(id);

            if (movie == null)
                return NotFound();

            // Находим все записи MovieGenre связанные с удаляемым фильмом
            var movieGenresToDelete = db.MovieGenre.Where(mg => mg.MovieId == id);

            // Удаляем эти записи из контекста
            db.MovieGenre.RemoveRange(movieGenresToDelete);

            // Удаляем фильм
            db.Movie.Remove(movie);

            // Сохраняем изменения
            await db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        public IActionResult Privacy()
        {
            return View();
        }
    }
}
