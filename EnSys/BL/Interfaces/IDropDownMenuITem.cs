using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Interfaces
{
    public interface IDropDownMenuITem
    {
        object Group { get; set; }
        int Value { get; set; }
        string Text { get; set; }
    }
}
