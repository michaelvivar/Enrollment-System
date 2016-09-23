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
    [Table("Instructor")]
    public class Instructor : Entity
    {
        [Required]
        public Status Status { get; set; }

        //public int CreatedBy { get; set; }
        //[ForeignKey("CreatedBy")]
        //public virtual User User { get; set; }

        public int PersonId { get; set; }
        [ForeignKey("PersonId")]
        public virtual Person Person { get; set; }
    }
}
