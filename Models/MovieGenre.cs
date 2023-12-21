using System.ComponentModel.DataAnnotations;

namespace MovieApplication.Models
{
    public class MovieGenre
    {

        // Внешний ключ для фильма
        [Key]
        public int MovieId { get; set; }
        public Movie Movie { get; set; }

        // Внешний ключ для жанра
        [Key]
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
    }
}
