using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Util.Enums;

namespace DL.Entities
{
    [Table("Course")]
    public class Course : Entity 
    {
        [Required]
        public string Code { get; set; }
    }
}
