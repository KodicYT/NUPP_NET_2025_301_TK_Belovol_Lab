using Library.Common;
using Library.Infrastructure;
using Library.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("=== ЛАБОРАТОРНА РОБОТА 3 ===");
        
        try
        {
            // Частина 1: Тестування Common сервісів
            Console.WriteLine("\n--- ТЕСТУВАННЯ COMMON СЕРВІСІВ ---");
            
            var stringService = new LibraryServiceAsync<string>();
            await stringService.CreateAsync("Тестовий рядок");
            var strings = await stringService.ReadAllAsync();
            Console.WriteLine($"✓ String Service: {strings.Count()} елементів");

            // Частина 2: Тестування Infrastructure з базою даних
            Console.WriteLine("\n--- ТЕСТУВАННЯ INFRASTRUCTURE ---");
            
            // Ініціалізація бази даних
            using var context = new LibraryContext();
            await context.Database.EnsureDeletedAsync(); // Видаляємо стару базу
            await context.Database.EnsureCreatedAsync(); // Створюємо нову
            Console.WriteLine("✓ База даних створена успішно!");

            // Додавання тестових даних БЕЗ сервісів (безпосередньо в контекст)
            Console.WriteLine("Додавання тестових даних...");
            
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

            var reader = new ReaderModel
            {
                Id = Guid.NewGuid(),
                Name = "Олексій Бєловол",
                BooksBorrowed = 1
            };

            // Додаємо безпосередньо в DbContext
            context.Books.Add(book);
            context.EBooks.Add(ebook);
            context.Journals.Add(journal);
            context.Readers.Add(reader);

            // Зберігаємо зміни
            await context.SaveChangesAsync();
            Console.WriteLine("✓ Тестові дані додані успішно!");

            // Тепер тестуємо сервіси на існуючих даних
            Console.WriteLine("\n--- ТЕСТУВАННЯ СЕРВІСІВ НА ДАНИХ З БАЗИ ---");

            var bookRepository = new Repository<BookModel>(context);
            var ebookRepository = new Repository<EBookModel>(context); 
            var journalRepository = new Repository<JournalModel>(context);
            var readerRepository = new Repository<ReaderModel>(context);
            
            var infrastructureBookService = new InfrastructureLibraryServiceAsync<BookModel>(bookRepository);
            var infrastructureEbookService = new InfrastructureLibraryServiceAsync<EBookModel>(ebookRepository);
            var infrastructureJournalService = new InfrastructureLibraryServiceAsync<JournalModel>(journalRepository);

            // Читання даних з бази через сервіси
            var dbBooks = await infrastructureBookService.ReadAllAsync();
            var dbEbooks = await infrastructureEbookService.ReadAllAsync();
            var dbJournals = await infrastructureJournalService.ReadAllAsync();
            var dbReaders = await readerRepository.GetAllAsync();

            Console.WriteLine($"✓ Книг в базі: {dbBooks.Count()}");
            Console.WriteLine($"✓ Електронних книг в базі: {dbEbooks.Count()}");  
            Console.WriteLine($"✓ Журналів в базі: {dbJournals.Count()}");
            Console.WriteLine($"✓ Читачів в базі: {dbReaders.Count()}");

            // Вивід деталей
            Console.WriteLine("\n--- ДАНІ З БАЗИ ДАНИХ ---");
            foreach (var dbBook in dbBooks)
            {
                Console.WriteLine($"[Book] {dbBook.Title} ({dbBook.Year}) - {dbBook.Author}, {dbBook.Pages} сторінок");
            }
            foreach (var dbEbook in dbEbooks)
            {
                Console.WriteLine($"[EBook] {dbEbook.Title} ({dbEbook.Year}) - {dbEbook.Author}, Формат: {dbEbook.Format}, {dbEbook.FileSizeMB} MB");
            }
            foreach (var dbJournal in dbJournals)
            {
                Console.WriteLine($"[Journal] {dbJournal.Title} №{dbJournal.IssueNumber} ({dbJournal.Year}) - {dbJournal.Publisher}");
            }
            foreach (var dbReader in dbReaders)
            {
                Console.WriteLine($"[Reader] {dbReader.Name}, взято книг: {dbReader.BooksBorrowed}");
            }

            // Тестування пагінації
            Console.WriteLine("\n--- ТЕСТУВАННЯ ПАГІНАЦІЇ ---");
            var pagedBooks = await infrastructureBookService.ReadAllAsync(1, 1);
            Console.WriteLine($"✓ Пагінація: {pagedBooks.Count()} елемент на сторінці 1");

            // Тестування пошуку за ID
            Console.WriteLine("\n--- ТЕСТУВАННЯ ПОШУКУ ЗА ID ---");
            var foundBook = await infrastructureBookService.ReadAsync(book.Id);
            Console.WriteLine($"✓ Знайдена книга: {foundBook?.Title}");

            Console.WriteLine("\n=== ЛАБОРАТОРНА РОБОТА 3 ВИКОНАНА УСПІШНО! ===");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\n=== ПОМИЛКА: {ex.Message} ===");
            Console.WriteLine($"Деталі: {ex.InnerException?.Message}");
        }

        Console.WriteLine("Натисніть будь-яку клавішу для виходу...");
        Console.ReadKey();
    }
}