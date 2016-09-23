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
    [Table("Room")]
    public class Room : Entity
    {
        [Required]
        public string Number { get; set; }
        [Required]
        public int Capacity { get; set; }
        public Status Status { get; set; }
    }
}
