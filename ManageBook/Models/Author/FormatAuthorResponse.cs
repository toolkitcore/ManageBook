using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ManageBook.Models
{
    public class FormatAuthorResponse
    {
        [Column("Id", Order = 0)]
        public int Id { get; set; }

        [MaxLength(255)]
        [Column("Name", Order = 1, TypeName = "nvarchar")]
        public string Name { get; set; }

        [Column("Female", Order = 2)]
        public Boolean Female { get; set; }

        [Column("BornYear", Order = 3)]
        public int BornYear { get; set; }

        [Column("DiedYear", Order = 4)]
        public int? DiedYear { get; set; }

        [Column("Age", Order = 5)]
        public int Age { get; set; }
    }
}
