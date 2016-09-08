using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL.Entities
{
    public class Student
    {
        [Key]
        public int Id { get; set; }
        public int PersonId { get; set; }
        [ForeignKey("PersonId")]
        public virtual Person Info { get; set; }
    }
}
