using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Infrastructure.Models
{
    [Table("BorrowRecords")]
    public class BorrowRecordModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid BookId { get; set; }

        [Required]
        public Guid ReaderId { get; set; }

        public DateTime BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        // Навігаційні властивості
        [ForeignKey("BookId")]
        public virtual BookModel? Book { get; set; }

        [ForeignKey("ReaderId")]
        public virtual ReaderModel? Reader { get; set; }
    }
}