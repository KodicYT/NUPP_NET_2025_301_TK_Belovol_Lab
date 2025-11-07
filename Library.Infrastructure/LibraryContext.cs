using Microsoft.EntityFrameworkCore;
using Library.Infrastructure.Models;

namespace Library.Infrastructure
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options)
        {
        }

        public DbSet<BookModel> Books { get; set; }
        public DbSet<EBookModel> EBooks { get; set; }
        public DbSet<JournalModel> Journals { get; set; }
        public DbSet<ReaderModel> Readers { get; set; }
        public DbSet<BorrowRecordModel> BorrowRecords { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source=library.db");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Fluent API конфігурація

            // Конфігурація для BookModel
            modelBuilder.Entity<BookModel>(entity =>
            {
                entity.HasKey(b => b.Id);
                entity.Property(b => b.Title).IsRequired().HasMaxLength(200);
                entity.Property(b => b.Author).IsRequired().HasMaxLength(100);
                
                // Table-per-Type для наслідування
                entity.ToTable("Books");
            });

            // Конфігурація для EBookModel
            modelBuilder.Entity<EBookModel>(entity =>
            {
                entity.HasBaseType<BookModel>();
                entity.Property(e => e.Format).HasMaxLength(10);
                entity.ToTable("EBooks");
            });

            // Конфігурація для JournalModel
            modelBuilder.Entity<JournalModel>(entity =>
            {
                entity.HasKey(j => j.Id);
                entity.Property(j => j.Title).IsRequired().HasMaxLength(200);
                entity.Property(j => j.Publisher).HasMaxLength(100);
            });

            // Конфігурація для ReaderModel
            modelBuilder.Entity<ReaderModel>(entity =>
            {
                entity.HasKey(r => r.Id);
                entity.Property(r => r.Name).IsRequired().HasMaxLength(100);
            });

            // Конфігурація для BorrowRecordModel
            modelBuilder.Entity<BorrowRecordModel>(entity =>
            {
                entity.HasKey(br => br.Id);
                
                // Зв'язок один-до-багатьох з BookModel
                entity.HasOne(br => br.Book)
                      .WithMany(b => b.BorrowRecords)
                      .HasForeignKey(br => br.BookId)
                      .OnDelete(DeleteBehavior.Cascade);

                // Зв'язок один-до-багатьох з ReaderModel
                entity.HasOne(br => br.Reader)
                      .WithMany(r => r.BorrowRecords)
                      .HasForeignKey(br => br.ReaderId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}