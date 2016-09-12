using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util.Enums;

namespace BL.Interfaces
{
    public interface IOption
    {
        int Id { get; set; }
        OptionType Type { get; set; }
        string Text { get; set; }
        int Value { get; set; }
        object Group { get; set; }
    }
}
