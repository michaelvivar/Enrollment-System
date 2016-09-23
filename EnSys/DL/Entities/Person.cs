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
    [Table("Person")]
    public class Person : Entity
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        public Gender Gender { get; set; }

        public int ContactInfoId { get; set; }
        public virtual ContactInfo ContactInfo { get; set; }

        [NotMapped]
        public override Status Status { get; set; }
        [NotMapped]
        public override string Remarks { get; set; }
        [NotMapped]
        public override DateTime? CreatedDate { get; set; }
    }
}
