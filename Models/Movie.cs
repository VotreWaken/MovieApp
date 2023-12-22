using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieApplication.Models
{
    public class Movie
    {
        // Id Movie
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        // Movie Title
        [Required(ErrorMessage = "Поле должно быть установлено")]
        [Display(Name = "Movie Title")]
        public string? MovieTitle { get; set; }

        // Movie Description
        [Required(ErrorMessage = "Поле должно быть установлено")]
        [Display(Name = "Movie Description")]
        public string? MovieDescription { get; set; }

        // Movie Image Path
        [DisplayName("Movie Image")]
        public string? FilmImage { get; set; }

        // Movie Director
        [Required(ErrorMessage = "Поле должно быть установлено")]
        [Display(Name = "Movie Director")]
        public string? Director { get; set; }


        // Production Date
        [Required(ErrorMessage = "Поле должно быть установлено")]
        [Display(Name = "Production Date")]
        [DataType(DataType.Date)]
        [CustomDateRange(ErrorMessage = "Дата производства должна быть не раньше 1900 года и не больше текущего года.")]
        public DateTime ProductionDate { get; set; }

        // Navigation Property to Link to Movies
        public ICollection<MovieGenre> MovieGenres { get; set; } = new List<MovieGenre>();

        // Property for Selected Genres
        // Ignore in the database
        [NotMapped]
        [Display(Name = "Genres")]
        public List<int> SelectedGenres { get; set; } = new List<int>();
    }
}
