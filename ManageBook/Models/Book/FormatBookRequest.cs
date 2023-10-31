using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ManageBook.Models
{
    public class FormatBookRequest
    {
        [MaxLength(255)]
        [Column("Title", Order = 0, TypeName = "nvarchar")]
        [Required]
        public string Title { get; set; }

        [MaxLength(255)]
        [Column("Topic", Order = 1, TypeName = "nvarchar")]
        public string Topic { get; set; }

        [Column("PublishYear", Order = 3)]
        public int PublishYear { get; set; }

        [Column("AuthorId", Order = 2)]
        [Required]
        public int AuthorId { get; set; }

        [Column("Price", Order = 4, TypeName = "numeric(10,2)")]
        [DefaultValue(0)]
        public Decimal Price { get; set; }

        [Column("Rating", Order = 5)]
        public Byte Rating { get; set; }
    }
}
