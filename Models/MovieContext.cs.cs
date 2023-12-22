using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

        // Configures the MovieGenre Entity in a Database Context.
        /* 
           Tells Entity Framework Core how to Properly set up a Table to Store a 
           many-to-many Relationship Between the Movie and Genre Entities via the 
           MovieGenre Staging Table.
        */
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // Defines a Composite Key for the Table Corresponding to the MovieGenre Entity
            // the Table will have a Composite Key Consisting of Two Columns 
            // MovieId and GenreId ( Many to Many )
            modelBuilder.Entity<MovieGenre>()
                .HasKey(e => new { e.MovieId, e.GenreId });

            // Passes Control to the Underlying Implementation of the 
            // OnModelCreating Method to the Parent Class
            base.OnModelCreating(modelBuilder);
        }

    }
}
