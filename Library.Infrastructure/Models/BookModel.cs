using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Infrastructure.Models
{
    [Table("Books")]
    public class BookModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Author { get; set; } = string.Empty;

        public int Year { get; set; }
        public int Pages { get; set; }

        // Зв'язок один-до-багатьох з BorrowRecordModel
        public virtual ICollection<BorrowRecordModel>? BorrowRecords { get; set; }
    }
}