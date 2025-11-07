using Library.Common;
using Library.Infrastructure;
using Library.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("=== ЛАБОРАТОРНА РОБОТА 3 - ТЕСТ БАЗИ ДАНИХ ===");
        
        try
        {
            // Ініціалізація бази даних з правильними параметрами
            var optionsBuilder = new DbContextOptionsBuilder<LibraryContext>();
            optionsBuilder.UseSqlite("Data Source=library.db");
            using var context = new LibraryContext(optionsBuilder.Options);
            
            await context.Database.EnsureDeletedAsync();
            await context.Database.EnsureCreatedAsync();
            Console.WriteLine("✓ База даних створена успішно!");

            // Додавання тестових даних
            var book = new BookModel 
            { 
                Id = Guid.NewGuid(), 
                Title = "Мистецтво війни", 
                Author = "Сунь-Цзи", 
                Year = 1999, 
                Pages = 250 
            };

            var ebook = new EBookModel 
            { 
                Id = Guid.NewGuid(),
                Title = "Програмування на C#", 
                Author = "Джон Сміт", 
                Year = 2021, 
                Pages = 350,
                Format = "PDF",
                FileSizeMB = 5.2
            };

            var journal = new JournalModel 
            { 
                Id = Guid.NewGuid(),
                Title = "Наука і техніка", 
                Year = 2023, 
                IssueNumber = 4, 
                Publisher = "Освіта України" 
            };

            // Додаємо безпосередньо в DbContext
            context.Books.Add(book);
            context.EBooks.Add(ebook);
            context.Journals.Add(journal);

            // Зберігаємо зміни
            await context.SaveChangesAsync();
            Console.WriteLine("✓ Тестові дані додані успішно!");

            // Перевірка даних
            Console.WriteLine($"✓ Книг в базі: {await context.Books.CountAsync()}");
            Console.WriteLine($"✓ Електронних книг в базі: {await context.EBooks.CountAsync()}");  
            Console.WriteLine($"✓ Журналів в базі: {await context.Journals.CountAsync()}");

            Console.WriteLine("\n=== ТЕСТУВАННЯ ЗАВЕРШЕНЕ УСПІШНО! ===");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\n=== ПОМИЛКА: {ex.Message} ===");
        }

        Console.WriteLine("Натисніть будь-яку клавішу для виходу...");
        Console.ReadKey();
    }
}