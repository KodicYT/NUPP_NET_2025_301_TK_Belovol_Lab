using System;

namespace Library.Common
{
    public class EBook : Book
    {
        public string Format { get; set; }
        public double FileSizeMB { get; set; }

        public EBook() { }

        public EBook(string title, string author, int year, int pages, string format, double fileSize)
            : base(title, author, year, pages)
        {
            Format = format;
            FileSizeMB = fileSize;
        }

        public override void DisplayInfo()
        {
            Console.WriteLine($"[EBook] {Title} ({Year}) — Автор: {Author}, Формат: {Format}, {FileSizeMB} MB");
        }
    }
}
