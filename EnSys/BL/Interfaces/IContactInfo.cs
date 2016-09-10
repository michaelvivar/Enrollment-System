using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Interfaces
{
    public interface IContactInfo
    {
        int ContactInfoId { get; set; }
        string Email { get; set; }
        string Telephone { get; set; }
        string Mobile { get; set; }
    }
}
