using BL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util.Enums;

namespace BL.Dto
{
    public class StudentDto : PersonDto, IStudent
    {
        public int Id { get; set; }
        public YearLevel Level { get; set; }
        public Status Status { get; set; }
        public int CourseId { get; set; }
        public string Course { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
