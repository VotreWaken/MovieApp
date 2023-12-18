namespace MovieApplication.Models
{
    public class MovieGenre
    {
        // Идентификатор связи между фильмом и жанром
        public int Id { get; set; }

        // Внешний ключ для фильма
        public int MovieId { get; set; }
        public Movie Movie { get; set; }

        // Внешний ключ для жанра
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
    }
}
