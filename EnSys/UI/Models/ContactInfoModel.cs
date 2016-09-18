using BL.Dto;
using System.ComponentModel.DataAnnotations;

namespace UI.Models
{
    public class ContactInfoModel : IContactInfo
    {
        public int? ContactInfoId { get; set; }
        [Display(Name = "Email Address")]
        public string Email { get; set; }
        [Display(Name = "Mobile No.")]
        public string Mobile { get; set; }
        [Display(Name = "Telephone No.")]
        public string Telephone { get; set; }
    }
}