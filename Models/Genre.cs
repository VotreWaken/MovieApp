namespace MovieApplication.Models
{
    public class Genre
    {
        // Id Genre
        public int Id { get; set; }

        // Genre Name
        public string? GenreName { get; set; }

        // Navigation Property to Link to Movies
        public ICollection<MovieGenre> MovieGenres { get; set; } = new List<MovieGenre>();
    }
}
