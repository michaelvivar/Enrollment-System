using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Util.Enums;

namespace DL.Entities
{
    [Table("Course")]
    public class Course
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Code { get; set; }
        public string Remarks { get; set; }
        [Required]
        public Status Status { get; set; }
    }
}
