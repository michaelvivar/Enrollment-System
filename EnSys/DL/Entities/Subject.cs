using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Util.Enums;

namespace DL.Entities
{
    [Table("Subject")]
    public class Subject
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public YearLevel Level { get; set; }
        [Required]
        public int Units { get; set; }
        public string Remarks { get; set; }
        [Required]
        public Status Status { get; set; }
    }
}
