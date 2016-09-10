using System.Collections.Generic;
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
