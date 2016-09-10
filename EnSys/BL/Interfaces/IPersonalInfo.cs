using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util.Enums;

namespace BL.Interfaces
{
    public interface IPersonalInfo : IContactInfo
    {
        int PersonId { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        DateTime BirthDate { get; set; }
        Gender Gender { get; set; }
    }
}
