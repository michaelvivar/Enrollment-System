using BL.Dto;
using Util.Enums;

namespace UI.Models
{
    public class OptionModel : IOption
    {
        public OptionType Type { get; set; }
        public string Text { get; set; }
        public int Value { get; set; }
    }
}