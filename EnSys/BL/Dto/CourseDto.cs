using Util.Enums;

namespace BL.Dto
{
    public class CourseDto : ICourse
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Remarks { get; set; }
        public int? Students { get; set; }
        public Status? Status { get; set; }
    }

    public interface ICourse
    {
        int Id { get; set; }
        string Code { get; set; }
        string Remarks { get; set; }
        Status? Status { get; set; }
        int? Students { get; set; }
    }
}
