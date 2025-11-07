using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Infrastructure.Models
{
    [Table("Readers")]
    public class ReaderModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        public int BooksBorrowed { get; set; }

        // Зв'язок один-до-багатьох з BorrowRecordModel
        public virtual ICollection<BorrowRecordModel>? BorrowRecords { get; set; }
    }
}