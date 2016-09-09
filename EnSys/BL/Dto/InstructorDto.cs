using BL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Dto
{
    public class InstructorDto : IInstructor
    {
        public int Id { get; set; }
        public IPersonalInfo PersonInfo { get; set; }
        public IContactInfo ContactInfo { get; set; }
    }
}
