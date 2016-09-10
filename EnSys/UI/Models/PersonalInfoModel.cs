using BL.Dto;
using BL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Util.Enums;

namespace UI.Models
{
    public class PersonalInfoModel : ContactInfoModel, IPersonalInfo
    {
        public int PersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public Gender Gender { get; set; }
    }
}