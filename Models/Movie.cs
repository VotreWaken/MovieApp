using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieApplication.Models
{
    public class Movie
    {
        // Идентификатор фильма
        [Key]
        [Column("ID")]
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

        // Свойство для выбранных жанров

        [NotMapped] // Игнорирование в базе данных
        [Display(Name = "Genres")]
        public List<int> SelectedGenres { get; set; } = new List<int>();
    }
}
