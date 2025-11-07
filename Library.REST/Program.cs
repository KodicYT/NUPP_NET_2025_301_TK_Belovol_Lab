using Library.Common;
using Library.Infrastructure;
using Library.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();

// Database Context
builder.Services.AddDbContext<LibraryContext>(options =>
    options.UseSqlite("Data Source=library.db"));

// Register Repositories
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

// Явна реєстрація сервісів
builder.Services.AddScoped<ICrudServiceAsync<BookModel>>(provider =>
{
    var context = provider.GetRequiredService<LibraryContext>();
    var repository = new Repository<BookModel>(context);
    return new LibraryServiceAsync<BookModel>(repository);
});

builder.Services.AddScoped<ICrudServiceAsync<EBookModel>>(provider =>
{
    var context = provider.GetRequiredService<LibraryContext>();
    var repository = new Repository<EBookModel>(context);
    return new LibraryServiceAsync<EBookModel>(repository);
});

builder.Services.AddScoped<ICrudServiceAsync<JournalModel>>(provider =>
{
    var context = provider.GetRequiredService<LibraryContext>();
    var repository = new Repository<JournalModel>(context);
    return new LibraryServiceAsync<JournalModel>(repository);
});

builder.Services.AddScoped<ICrudServiceAsync<ReaderModel>>(provider =>
{
    var context = provider.GetRequiredService<LibraryContext>();
    var repository = new Repository<ReaderModel>(context);
    return new LibraryServiceAsync<ReaderModel>(repository);
});

var app = builder.Build();

// Configure the HTTP request pipeline
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Initialize database
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<LibraryContext>();
    context.Database.EnsureCreated();
}

app.Run();