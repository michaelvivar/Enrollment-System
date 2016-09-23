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
    [Table("Option")]
    public class Option : Entity
    {
        [Required]
        public OptionType Type { get; set; }
        [Required]
        public string Text { get; set; }
        [Required]
        public int Value { get; set; }
        public string Group { get; set; }
    }
}
