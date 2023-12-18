namespace MovieApplication.Models
{
    public class Genre
    {
        // Идентификатор жанра
        public int Id { get; set; }
        // Название жанра
        public string? GenreName { get; set; }

        // Навигационное свойство для связи с фильмами
        public ICollection<MovieGenre> MovieGenres { get; set; } = new List<MovieGenre>();
    }
}
