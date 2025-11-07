using System;

namespace Library.Common
{
    public class Reader
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int BooksBorrowed { get; set; }

        // Статичне поле
        public static int TotalReaders = 0;

        // Подія
        public event Action<string>? BookReturned;

        public Reader(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
            BooksBorrowed = 0;
            TotalReaders++;
        }

        public void BorrowBook(Item item)
        {
            BooksBorrowed++;
            Console.WriteLine($"{Name} взяв книгу: {item.Title}");
        }

        public void ReturnBook(string bookTitle)
        {
            BooksBorrowed--;
            BookReturned?.Invoke($"{Name} повернув книгу '{bookTitle}'");
        }
    }
}
