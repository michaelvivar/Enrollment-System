using BL.Dto;
using System;
using System.ComponentModel.DataAnnotations;
using Util.Enums;
using Util.Helpers;

namespace UI.Models
{
    public class PersonalInfoModel : ContactInfoModel, IPersonalInfo
    {
        public int PersonId { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Birth Date")]
        public DateTime? BirthDate { get; set; }
        public double Age
        {
            get
            {
                return BirthDate.Age();
            }
        }
        public Gender? Gender { get; set; }
        public int GenderId
        {
            get
            {
                return Convert.ToInt32(Gender);
            }
        }
    }
}