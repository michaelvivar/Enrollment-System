using BL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util.Enums;

namespace BL.Dto
{
    public class InstructorDto : PersonDto, IInstructor
    {
        public int Id { get; set; }
        public Status Status { get; set; }
    }
}
