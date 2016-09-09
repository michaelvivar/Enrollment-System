using System.ComponentModel.DataAnnotations;
using Util.Enums;

namespace DL.Entities
{
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
