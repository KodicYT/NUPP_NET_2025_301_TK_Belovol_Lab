namespace Library.REST.Models
{
    public class EBookDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public int Year { get; set; }
        public int Pages { get; set; }
        public string Format { get; set; } = string.Empty;
        public double FileSizeMB { get; set; }
    }

    public class CreateEBookDto
    {
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public int Year { get; set; }
        public int Pages { get; set; }
        public string Format { get; set; } = string.Empty;
        public double FileSizeMB { get; set; }
    }
}