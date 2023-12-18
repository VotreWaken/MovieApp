using Microsoft.EntityFrameworkCore;

namespace MovieApplication.Models
{
    public class MovieContext : DbContext
    {
        public DbSet<Movie> Movie { get; set; }
        public DbSet<Genre> Genre { get; set; }
        public DbSet<MovieGenre> MovieGenre { get; set; }
        public MovieContext(DbContextOptions<MovieContext> options)
           : base(options)
        {
            //if (Database.EnsureCreated())
            //{
            //    Movies?.Add(new Movie { Name = "Богдан", Surname = "Иваненко", Age = 20, GPA = 10.5 });
            //    Movies?.Add(new Movie { Name = "Анна", Surname = "Шевченко", Age = 23, GPA = 11.5 });
            //    Movies?.Add(new Movie { Name = "Петро", Surname = "Петренко", Age = 25, GPA = 12 });
            //    SaveChanges();
            //}
        }
    }
}
