using BL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util.Enums;

namespace BL.Dto
{
    public class OptionDto : IOption
    {
        public int Id { get; set; }
        public OptionType Type { get; set; }
        public string Text { get; set; }
        public int Value { get; set; }
        public object Group { get; set; }
    }
}
