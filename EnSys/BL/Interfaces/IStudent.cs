using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util.Enums;

namespace BL.Interfaces
{
    public interface IStudent : IPersonalInfo
    {
        int Id { get; set; }
        YearLevel Level { get; set; }
        Status Status { get; set; }
        DateTime CreatedDate { get; set; }
        int CourseId { get; set; }
        string Course { get; set; }
    }
}
