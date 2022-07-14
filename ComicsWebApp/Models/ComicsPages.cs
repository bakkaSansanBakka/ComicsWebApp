namespace ComicsWebApp.Models
{
    public class ComicsPages
    {
        public int Id { get; set; }
        public byte[] Content { get; set; }

        public int ComicsId { get; set; }
        public Comics Comics { get; set; }
    }
}
