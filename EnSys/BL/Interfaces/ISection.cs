using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util.Enums;

namespace BL.Interfaces
{
    public interface ISection
    {
        int Id { get; set; }
        string Code { get; set; }
        string Remarks { get; set; }
        YearLevel? Level { get; set; }
        Status? Status { get; set; }
        int Students { get; set; }
    }
}
