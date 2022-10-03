namespace ComicsWebApp.Data.Models
{
    public class ComicsComicsGenres
    {
        public int ComicsId { get; set; }
        public int GenresId { get; set; }

        public Comics Comics { get; private set; }
        public ComicsGenre ComicsGenre { get; private set; }
    }
}
