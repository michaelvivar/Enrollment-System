using System;

namespace BL.Dto
{
    public class ClassScheduleDto : IClassSchedule
    {
        public int Id { get; set; }
        public DayOfWeek? Day { get; set; }
        public int?[] Days { get; set; }
        public DateTime? TimeStart { get; set; }
        public DateTime? TimeEnd { get; set; }
        public int? Capacity { get; set; }
        public string Remarks { get; set; }

        public int? InstructorId { get; set; }
        public string InstructorFirstName { get; set; }
        public string InstructorLastName { get; set; }

        public int? SubjectId { get; set; }
        public string Subject { get; set; }

        public int? SectionId { get; set; }
        public string Section { get; set; }

        public int? RoomId { get; set; }
        public string Room { get; set; }
    }

    public interface IClassSchedule
    {
        int Id { get; set; }
        DayOfWeek? Day { get; set; }
        int?[] Days { get; set; }
        DateTime? TimeStart { get; set; }
        DateTime? TimeEnd { get; set; }
        int? Capacity { get; set; }
        string Remarks { get; set; }

        int? InstructorId { get; set; }
        string InstructorFirstName { get; set; }
        string InstructorLastName { get; set; }

        int? SubjectId { get; set; }
        string Subject { get; set; }

        int? SectionId { get; set; }
        string Section { get; set; }

        int? RoomId { get; set; }
        string Room { get; set; }
    }
}
