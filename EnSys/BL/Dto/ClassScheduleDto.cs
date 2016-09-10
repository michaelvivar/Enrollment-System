using BL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Dto
{
    public class ClassScheduleDto : IClassSchedule
    {
        public int Id { get; set; }
        public DayOfWeek Day { get; set; }
        public IList<DayOfWeek> Days { get; set; }
        public DateTime TimeStart { get; set; }
        public DateTime TimeEnd { get; set; }
        public int Capacity { get; set; }
        public string Remarks { get; set; }

        public int InstructorId { get; set; }
        public string InstructorFirstName { get; set; }
        public string InstructorLastName { get; set; }

        public int SubjectId { get; set; }
        public string Subject { get; set; }

        public int RoomId { get; set; }
        public string Room { get; set; }
    }
}
