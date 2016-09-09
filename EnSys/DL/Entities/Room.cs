using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util.Enums;

namespace DL.Entities
{
    public class Room
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Number { get; set; }
        public string Remarks { get; set; }
        [Required]
        public int Capacity { get; set; }
        public Status Status { get; set; }
    }
}
