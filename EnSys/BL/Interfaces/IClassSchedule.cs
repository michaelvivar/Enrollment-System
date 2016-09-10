using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Interfaces
{
    public interface IClassSchedule
    {
        int Id { get; set; }
        DayOfWeek Day { get; set; }
        IList<DayOfWeek> Days { get; set; }
        DateTime TimeStart { get; set; }
        DateTime TimeEnd { get; set; }
        int Capacity { get; set; }
        string Remarks { get; set; }

        int InstructorId { get; set; }
        string InstructorFirstName { get; set; }
        string InstructorLastName { get; set; }

        int SubjectId { get; set; }
        string Subject { get; set; }

        int RoomId { get; set; }
        string Room { get; set; }
    }
}
