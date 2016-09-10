using BL.Interfaces;
using System;
using Util.Enums;

namespace BL.Dto
{
    public class PersonDto : ContactInfoDto, IPersonalInfo
    {
        public int PersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public Gender Gender { get; set; }
    }
}
