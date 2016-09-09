using System.ComponentModel.DataAnnotations;
using Util.Enums;

namespace DL.Entities
{
    public class Subject
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public int Units { get; set; }
        public string Remarks { get; set; }
        public Status Status { get; set; }
    }
}
