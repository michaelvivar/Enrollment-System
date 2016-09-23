using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL.Entities
{
    [Table("ContactInfo")]
    public class ContactInfo : Entity
    {
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string Mobile { get; set; }
        [NotMapped]
        public override DateTime? CreatedDate { get; set; }
    }
}
