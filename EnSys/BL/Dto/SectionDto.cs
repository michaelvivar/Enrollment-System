using Util.Enums;

namespace BL.Dto
{
    public class SectionDto : ISection
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Remarks { get; set; }
        public YearLevel? Level { get; set; }
        public Status? Status { get; set; }
        public int? Students { get; set; }
    }

    public interface ISection
    {
        int Id { get; set; }
        string Code { get; set; }
        string Remarks { get; set; }
        YearLevel? Level { get; set; }
        Status? Status { get; set; }
        int? Students { get; set; }
    }
}
