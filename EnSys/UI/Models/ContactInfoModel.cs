using BL.Dto;
using BL.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UI.Models
{
    public class ContactInfoModel : IContactInfo
    {
        public int ContactInfoId { get; set; }
        [Display(Name = "Email Address")]
        public string Email { get; set; }
        [Display(Name = "Mobile No.")]
        public string Mobile { get; set; }
        [Display(Name = "Telephone No.")]
        public string Telephone { get; set; }
    }
}