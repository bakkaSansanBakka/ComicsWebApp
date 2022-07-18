namespace ComicsWebApp.Models
{
    public class ComicsPages
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] Content { get; set; }
        public string FileType { get; set; }

        public int ComicsId { get; set; }
        public Comics Comics { get; set; }
    }
}
