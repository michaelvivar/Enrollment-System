using BL;
using BL.Dto;
using BL.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;
using Util.Enums;

namespace UI.Models
{
    public class StudentModel : PersonalInfoModel, IStudent
    {
        public int Id { get; set; }
        [Display(Name = "Year Level")]
        public YearLevel? Level { get; set; }
        public int LevelId { get { return Convert.ToInt32(Level); } }
        public Status? Status { get; set; }
        public int StatusId { get { return Convert.ToInt32(Status); } }
        [Display(Name = "Course")]
        public int? CourseId { get; set; }
        public string Course { get; set; }
        [Display(Name = "Section")]
        public int? SectionId { get; set; }
        public string SectionCode { get; set; }
        [DataType(DataType.Date)] [Display(Name = "Date Enrolled")]
        public DateTime? CreatedDate { get; set; }
    }

    public class ValidateStudentModel : StudentModel, IValidatableObject
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

            if (string.IsNullOrWhiteSpace(Email))
            {
                hasError = true;
                yield return new ValidationResult(string.Format("Invalid email address!"));
            }

            if (!hasError)
            {
                List<ValidationResult> result = new List<ValidationResult>();
                Transaction.Scope(scope =>
                {
                    scope.Service<ValidatorService>(service =>
                    {
                        if (service.CheckPersonExists(PersonId, FirstName, LastName, BirthDate))
                            result.Add(new ValidationResult(string.Format("Person with name \"{0} {1}\" and birth date \"{2}\" is already exists!", FirstName, LastName, ((DateTime)BirthDate).ToShortDateString())));

                        if (service.CheckEmailExists(ContactInfoId, Email))
                            result.Add(new ValidationResult(string.Format("Email address \"{0}\" is already exists!", Email)));
                    });
                });

                foreach (var i in result)
                    yield return i;
            }

            //yield return new ValidationResult(string.Format("Name \"{0}\" is already exists!", FirstName), new[] { nameof(FirstName), nameof(LastName) });
        }
    }
}