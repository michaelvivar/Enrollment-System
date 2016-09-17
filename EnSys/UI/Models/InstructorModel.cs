using BL;
using BL.Dto;
using BL.Interfaces;
using BL.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using Util.Enums;

namespace UI.Models
{
    public class InstructorModel : PersonalInfoModel, IInstructor
    {
        public int Id { get; set; }
        [Required]
        public Status Status { get; set; }
        public int StatusId { get { return Convert.ToInt32(Status); } }
        [DataType(DataType.Date)] [Display(Name = "Date Enrolled")]
        public DateTime CreatedDate { get; set; }
    }

    public class ValidateInstructorModel : InstructorModel, IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            bool hasError = false;

            if (string.IsNullOrEmpty(Email) && string.IsNullOrEmpty(Telephone) && string.IsNullOrEmpty(Mobile))
            {
                hasError = true;
                yield return new ValidationResult(string.Format("Please fill atleast one of the contact information"));
            }

            if (BirthDate < (DateTime)SqlDateTime.MinValue && BirthDate > DateTime.Now)
            {
                hasError = true;
                yield return new ValidationResult(string.Format("Invalid date of birth!"));
            }

            if (!hasError)
            {
                bool[] result = new bool[2];
                Transaction.Scope(scope =>
                {
                    scope.Service<ValidatorService>(service =>
                    {
                        result[0] = service.CheckPersonExists(PersonId, FirstName, LastName, BirthDate);
                        result[1] = service.CheckEmailExists(ContactInfoId, Email);
                    });
                    scope.Service<StudentService>(service =>
                    {

                    });
                });

                if (result[0])
                    yield return new ValidationResult(string.Format("Person with name \"{0} {1}\" and birth date \"{2}\" is already exists!", FirstName, LastName, BirthDate.ToShortDateString()));

                if (result[1])
                    yield return new ValidationResult(string.Format("Email address \"{0}\" is already exists!", Email));
            }
        }
    }
}