using System;

namespace Library.Common
{
    public class Journal : Item
    {
        public int IssueNumber { get; set; }
        public string Publisher { get; set; }

        public Journal() { }

        public Journal(string title, int year, int issueNumber, string publisher)
        {
            Title = title;
            Year = year;
            IssueNumber = issueNumber;
            Publisher = publisher;
        }

        public override void DisplayInfo()
        {
            Console.WriteLine($"[Journal] {Title} №{IssueNumber} ({Year}) — {Publisher}");
        }
    }
}
