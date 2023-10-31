using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ManageBook.Models
{
    public class FormatBookResponse
    {
        [Column("Id", Order = 0)]
        public int Id { get; set; }

        [MaxLength(255)]
        [Column("Title", Order = 1, TypeName = "nvarchar")]
        public string Title { get; set; }

        [MaxLength(255)]
        [Column("Topic", Order = 2, TypeName = "nvarchar")]
        public string Topic { get; set; }

        [Column("AuthorName", Order = 3)]
        public string AuthorName { get; set; }

        [Column("PublishYear", Order = 4)]
        public int PublishYear { get; set; }

        [Column("Price", Order = 5)]
        public Decimal Price { get; set; }

        [Column("Rating", Order = 6)]
        public Byte Rating { get; set; }
    }
}
