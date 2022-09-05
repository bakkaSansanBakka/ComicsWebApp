namespace ComicsWebApp.Models
{
    public class ComicsGenreDTO
    {
        public int Id { get; set; }
        public string GenreName { get; set; }
        public List<ComicsViewModel>? Comics { get; set; } = new List<ComicsViewModel>();
    }
}
