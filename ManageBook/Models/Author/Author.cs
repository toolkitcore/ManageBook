using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ManageBook.Models
{
    public class Author
    {
        [Key]
        [Column("Id", Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(255)]
        [Column("Name", Order = 1, TypeName = "nvarchar")]
        public string Name { get; set; }

        [Column("Female", Order = 2)]
        public Boolean Female { get; set; }

        [Column("Born", Order = 3)]
        public int Born { get; set; }

        [Column("Died", Order = 4)]
        public int? Died { get; set; }

        public ICollection<Book> Book;
    }
}
