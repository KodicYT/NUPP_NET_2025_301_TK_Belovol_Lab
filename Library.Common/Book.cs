using System;

namespace Library.Common
{
    public class Book : Item
    {
        public string Author { get; set; }
        public int Pages { get; set; }

        // Конструктор
        public Book() { }

        public Book(string title, string author, int year, int pages)
        {
            Title = title;
            Author = author;
            Year = year;
            Pages = pages;
        }

        // Перевизначений метод
        public override void DisplayInfo()
        {
            Console.WriteLine($"[Book] {Title} ({Year}) — Автор: {Author}, {Pages} сторінок");
        }
    }
}
