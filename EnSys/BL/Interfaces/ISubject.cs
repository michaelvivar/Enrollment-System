using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util.Enums;

namespace BL.Interfaces
{
    public interface ISubject
    {
        int Id { get; set; }
        string Code { get; set; }
        Unit Units { get; set; }
        string Remarks { get; set; }
        Status Status { get; set; }
        YearLevel Level { get; set; }
    }
}
