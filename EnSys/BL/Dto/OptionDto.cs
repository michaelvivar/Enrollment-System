using Util.Enums;

namespace BL.Dto
{
    public class OptionDto : IOption
    {
        public OptionType Type { get; set; }
        public string Text { get; set; }
        public int Value { get; set; }
    }

    public interface IOption
    {
        OptionType Type { get; set; }
        string Text { get; set; }
        int Value { get; set; }
    }
}
