using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util.Enums;

namespace BL.Interfaces
{
    public interface IInstructor
    {
        int Id { get; set; }
        IPersonInfo PersonInfo { get; set; }
        IContactInfo ContactInfo { get; set; }
    }
}
