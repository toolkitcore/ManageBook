using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ManageBook.Models
{
    public class Book
    {
        [Key]
        [Column("Id", Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(255)]
        [Column("Title", Order = 1, TypeName = "nvarchar")]
        public string Title { get; set; }

        [MaxLength(255)]
        [Column("Topic", Order = 2, TypeName = "nvarchar")]
        public string Topic { get; set; }

        [Column("PublishYear", Order = 3)]
        public int PublishYear { get; set; }

        [Column("AuthorId", Order = 4)]
        [ForeignKey("Author")]
        public int AuthorId { get; set; }
        public Author Author { get; set; }

        [Column("Price", Order = 5, TypeName = "numeric(10,2)")]
        public Decimal Price { get; set; }

        [Column("Rating", Order = 6)]
        public Byte Rating { get; set; }
    }
}
