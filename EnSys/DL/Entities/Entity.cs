using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util.Enums;

namespace DL.Entities
{
    public abstract class Entity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public virtual Status Status { get; set; }
        public virtual string Remarks { get; set; }
        public virtual DateTime? CreatedDate { get; set; }
    }
}
