namespace Library.REST.Models
{
    public class ReaderDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int BooksBorrowed { get; set; }
    }

    public class CreateReaderDto
    {
        public string Name { get; set; } = string.Empty;
        public int BooksBorrowed { get; set; }
    }
}