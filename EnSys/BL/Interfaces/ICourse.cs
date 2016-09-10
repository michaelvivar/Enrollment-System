using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util.Enums;

namespace BL.Interfaces
{
    public interface ICourse
    {
        int Id { get; set; }
        string Code { get; set; }
        string Remarks { get; set; }
        Status Status { get; set; }
        IEnumerable<ISubject> Subjects { get; set; }
    }
}
