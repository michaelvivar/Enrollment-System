using System;
using Util.Enums;

namespace BL.Dto
{
    public class StudentDto : PersonDto, IStudent
    {
        public int Id { get; set; }
        public YearLevel? Level { get; set; }
        public Status? Status { get; set; }
        public int? CourseId { get; set; }
        public string Course { get; set; }
        public int? SectionId { get; set; }
        public string SectionCode { get; set; }
        public DateTime? CreatedDate { get; set; }
    }

    public interface IStudent : IPersonalInfo
    {
        int Id { get; set; }
        YearLevel? Level { get; set; }
        Status? Status { get; set; }
        DateTime? CreatedDate { get; set; }
        int? CourseId { get; set; }
        string Course { get; set; }
        int? SectionId { get; set; }
        string SectionCode { get; set; }
    }
}
