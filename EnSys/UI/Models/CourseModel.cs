using BL;
using BL.Dto;
using BL.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Util.Enums;

namespace UI.Models
{
    public class CourseModel :  ICourse
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Remarks { get; set; }
        public Status? Status { get; set; }
        public int StatusId { get { return Convert.ToInt32(Status); } }
        public int? Students { get; set; }
        public IEnumerable<ISubject> Subjects { get; set; }
    }

    public class ValidateCourseModel : CourseModel, IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            bool hasError = false;

            if (string.IsNullOrWhiteSpace(Code) || string.IsNullOrEmpty(Code))
            {
                hasError = true;
                yield return new ValidationResult("Code field is required", new[] { nameof(Code) });
            }

            if (Status == null || Status <= 0)
            {
                hasError = true;
                yield return new ValidationResult("Status field is required", new[] { nameof(Status) });
            }

            if (hasError)
                yield break;

            if (Transaction.Scope(scope => scope.Service<CourseValidatorService, bool>(service => service.CheckCourseCodeExists(Id, Code))))
                yield return new ValidationResult(string.Format("Course \"{0}\" is already exists!", Code), new[] { nameof(Code) });
        }
    }
}