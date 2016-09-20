using BL;
using BL.Dto;
using BL.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UI.Helpers;
using Util.Enums;

namespace UI.Models
{
    public class InstructorModel : PersonalInfoModel, IInstructor
    {
        public int Id { get; set; }
        public Status? Status { get; set; }
        public int StatusId { get { return Convert.ToInt32(Status); } }
        [DataType(DataType.Date)]
        [Display(Name = "Date Enrolled")]
        public DateTime? CreatedDate { get; set; }
        public string ContactInfo
        {
            get { return "Contact Information"; }
        }
    }

    public class ValidateInstructorModel : InstructorModel, IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            IValidationResultHelper<InstructorModel> helper = new ValidationResultHelper<InstructorModel>(this);

            helper.Validate(model => model.FirstName).Required(true).NotEmpty().ErrorMsg("First Name field is required");

            helper.Validate(model => model.LastName).Required(true).NotEmpty().ErrorMsg("Last Name field is required");

            helper.Validate(model => model.BirthDate).Required(true).LessThan(DateTime.Now).ErrorMsg("Invalid date of birth");

            helper.Validate(model => model.Gender).Required(true).GreaterThan(0).ErrorMsg("Gender field is required");

            helper.Validate(model => model.Status).Required(true).GreaterThan(0).ErrorMsg("Status field is required");

            helper.Validate(model => model.Email).Required(false).EmailAddress().ErrorMsg("Invalid email address");

            helper.IF(string.IsNullOrEmpty(Email) && string.IsNullOrEmpty(Telephone) && string.IsNullOrEmpty(Mobile)).ErrorMsg("Please fill atleast one of the contact information");

            if (helper.Errors.Count == 0)
            {
                Transaction.Scope(scope => scope.Service<ValidatorService>(service =>
                {
                    helper.Validate(model => model.FirstName).Required(true).IF(service.CheckPersonExists(Id, FirstName, LastName, BirthDate))
                        .ErrorMsg(string.Format("Person with name \"{0} {1}\" and birth date \"{2}\" is already exists!", FirstName, LastName, ((DateTime)BirthDate).ToShortDateString()));

                    helper.Validate(model => model.Email).Required(false).IF(service.CheckEmailExists(ContactInfoId, Email))
                        .ErrorMsg(string.Format("Email address \"{0}\" is already exists!", Email));
                }));
            }

            if (helper.Errors.Count > 0)
            {
                foreach (var error in helper.Errors)
                    yield return error;
            }
        }
    }
}