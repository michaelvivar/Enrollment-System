using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util.Enums;

namespace DL.Entities
{
    [Table("Section")]
    public class Section : Entity
    {
        [Required]
        public string Code { get; set; }
        [Required]
        public YearLevel Level { get; set; }
        [Required]
        public Status Status { get; set; }
    }
}
