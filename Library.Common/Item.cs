using System;

namespace Library.Common
{
    public class Item
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }

        // Метод
        public virtual void DisplayInfo()
        {
            Console.WriteLine($"Назва: {Title}, Рік: {Year}");
        }
    }
}