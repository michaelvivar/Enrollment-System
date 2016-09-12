using BL.Dto;
using BL.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Util.Enums;

namespace UI.Models
{
    public class PersonalInfoModel : ContactInfoModel, IPersonalInfo
    {
        public int PersonId { get; set; }
        [Display(Name = "First Name")] [Required]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")] [Required]
        public string LastName { get; set; }
        [Display(Name = "Birth Date")] [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        public Gender Gender { get; set; }
        public int GenderId
        {
            get
            {
                return Convert.ToInt32(Gender);
            }
        }
    }
}