namespace Library.REST.Models
{
    public class JournalDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int Year { get; set; }
        public int IssueNumber { get; set; }
        public string Publisher { get; set; } = string.Empty;
    }

    public class CreateJournalDto
    {
        public string Title { get; set; } = string.Empty;
        public int Year { get; set; }
        public int IssueNumber { get; set; }
        public string Publisher { get; set; } = string.Empty;
    }
}