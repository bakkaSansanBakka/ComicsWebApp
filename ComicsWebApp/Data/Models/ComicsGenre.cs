namespace ComicsWebApp.Data.Models
{
    public class ComicsGenre
    {
        public int Id { get; set; }
        public string GenreName { get; set; }
        public List<Comics>? Comics { get; set; } = new List<Comics>();
    }
}
