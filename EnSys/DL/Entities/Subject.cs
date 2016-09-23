using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Util.Enums;

namespace DL.Entities
{
    [Table("Subject")]
    public class Subject : Entity
    {
        [Required]
        public string Code { get; set; }
        [Required]
        public YearLevel Level { get; set; }
        [Required]
        public Unit Units { get; set; }
        [Required]
        public Status Status { get; set; }
    }
}
