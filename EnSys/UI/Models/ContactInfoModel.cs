using BL.Dto;
using BL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UI.Models
{
    public class ContactInfoModel : IContactInfo
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string Mobile { get; set; }

        public string Telephone { get; set; }
    }
}