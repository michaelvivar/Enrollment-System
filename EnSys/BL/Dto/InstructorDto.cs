using System;
using Util.Enums;

namespace BL.Dto
{
    public class InstructorDto : PersonDto, IInstructor
    {
        public int Id { get; set; }
        public Status? Status { get; set; }
        public DateTime? CreatedDate { get; set; }
    }

    public interface IInstructor : IPersonalInfo
    {
        int Id { get; set; }
        Status? Status { get; set; }
        DateTime? CreatedDate { get; set; }
    }
}
