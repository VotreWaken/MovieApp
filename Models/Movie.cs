namespace MovieApplication.Models
{
    public class Movie
    {
        // Идентификатор фильма
        public int Id { get; set; }
        // Название фильма
        public string? MovieTitle { get; set; }
        // Описание фильма
        public string? MovieDescription { get; set; }
        // Путь к изображению
        public string? FilmImage { get; set; }
        // Режиссер фильма
        public string? Director { get; set; }
        // Дата производства
        public DateTime ProductionDate { get; set; }

        // Навигационное свойство для связи с жанрами
        public ICollection<MovieGenre> MovieGenres { get; set; } = new List<MovieGenre>();
    }
}
