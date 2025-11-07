using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Infrastructure.Models
{
    [Table("Journals")]
    public class JournalModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        public int Year { get; set; }
        
        public int IssueNumber { get; set; }

        [MaxLength(100)]
        public string Publisher { get; set; } = string.Empty;

        // Зв'язок один-до-багатьох з BorrowRecordModel
        public virtual ICollection<BorrowRecordModel>? BorrowRecords { get; set; }
    }
}