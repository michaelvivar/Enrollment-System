using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util.Enums;

namespace BL.Dto
{
    public class StudentDto : PersonDto
    {
        public int Id { get; set; }
        public YearLevel Level { get; set; }
    }
}
