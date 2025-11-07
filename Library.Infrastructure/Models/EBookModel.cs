using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Infrastructure.Models
{
    [Table("EBooks")]
    public class EBookModel : BookModel
    {
        [MaxLength(10)]
        public string Format { get; set; } = string.Empty;
        
        public double FileSizeMB { get; set; }
    }
}