using Util.Enums;

namespace BL.Dto
{
    public class SubjectDto : ISubject
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Remarks { get; set; }
        public Unit? Units { get; set; }
        public YearLevel? Level { get; set; }
        public Status? Status { get; set; }
    }

    public interface ISubject
    {
        int Id { get; set; }
        string Code { get; set; }
        Unit? Units { get; set; }
        string Remarks { get; set; }
        YearLevel? Level { get; set; }
        Status? Status { get; set; }
    }
}
