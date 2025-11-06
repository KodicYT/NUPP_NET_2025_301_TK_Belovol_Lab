using Library.Common;

// Створення сервісу
var service = new LibraryService<Item>();

// Додавання елементів
var book = new Book("Мистецтво війни", "Сунь-Цзи", 1999, 250);
var ebook = new EBook("Програмування на C#", "Джон Сміт", 2021, 350, "PDF", 5.2);
var journal = new Journal("Наука і техніка", 2023, 4, "Освіта України");

service.Create(book);
service.Create(ebook);
service.Create(journal);

// Вивід усіх елементів
foreach (var item in service.ReadAll())
{
    item.DisplayInfo();
    Console.WriteLine(item.IsOld() ? "Це стара книга." : "Сучасне видання.");
    Console.WriteLine();
}

// Перевірка подій
var reader = new Reader("Олексій");
reader.BookReturned += msg => Console.WriteLine($"Подія: {msg}");
reader.BorrowBook(book);
reader.ReturnBook(book.Title);
