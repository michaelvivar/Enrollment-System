using BL.Interfaces;
using System.Collections.Generic;
using Util.Enums;

namespace BL.Dto
{
    public class CourseDto : ICourse
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Remarks { get; set; }
        public Status Status { get; set; }
        public IEnumerable<ISubject> Subjects { get; set; }
    }
}
