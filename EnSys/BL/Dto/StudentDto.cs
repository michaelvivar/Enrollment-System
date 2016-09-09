using BL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util.Enums;

namespace BL.Dto
{
    public class StudentDto : IStudent
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public YearLevel Level { get; set; }
        public Status Status { get; set; }
        public int CourseId { get; set; }
        public string Course { get; set; }
        public DateTime CreatedDate { get; set; }
    }

    public class StudentWithPersonInfoDto : StudentDto, IStudentWithPersonInfo
    {
        public IPersonInfo PersonInfo { get; set; }
        public IContactInfo ContactInfo { get; set; }
    }
}
