using System.ComponentModel;
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
        [Required(ErrorMessage = "Поле должно быть установлено")]
        [Display(Name = "Movie Title")]
        public string? MovieTitle { get; set; }
        // Описание фильма
        [Required(ErrorMessage = "Поле должно быть установлено")]
        [Display(Name = "Movie Description")]
        public string? MovieDescription { get; set; }
        // Путь к изображению
        [DisplayName("Movie Image")]
        public string? FilmImage { get; set; }
        // Режиссер фильма
        [Required(ErrorMessage = "Поле должно быть установлено")]
        [Display(Name = "Movie Director")]
        public string? Director { get; set; }
        // Дата производства
        [Required(ErrorMessage = "Поле должно быть установлено")]
        [Display(Name = "Production Date")]
        [DataType(DataType.Date)]
        [CustomDateRange(ErrorMessage = "Дата производства должна быть не раньше 1900 года и не больше текущего года.")]

        //[DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        //[Range(typeof(DateTime), "1900-01-01", "9999-12-31", ErrorMessage = "Дата производства должна быть не раньше 1900 года и не больше текущего года.")]
        public DateTime ProductionDate { get; set; }

        // Навигационное свойство для связи с жанрами
        public ICollection<MovieGenre> MovieGenres { get; set; } = new List<MovieGenre>();

        // Свойство для выбранных жанров

        [NotMapped] // Игнорирование в базе данных
        [Display(Name = "Genres")]
        public List<int> SelectedGenres { get; set; } = new List<int>();
    }
}
