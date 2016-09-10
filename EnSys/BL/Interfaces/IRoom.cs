using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util.Enums;

namespace BL.Interfaces
{
    public interface IRoom
    {
        int Id { get; set; }
        string Number { get; set; }
        string Remarks { get; set; }
        int Capacity { get; set; }
        Status Status { get; set; }
    }
}
