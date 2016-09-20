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
            IValidationResultHelper<CourseModel> helper = new ValidationResultHelper<CourseModel>(this);

            helper.Validate(model => model.Code).Required(true).ErrorMsg("Code field is required");

            helper.Validate(model => model.Status).Required(true).GreaterThan(0).ErrorMsg("Status field is required");

            if (!helper.Failed)
            {
                Transaction.Scope(scope => scope.Service<CourseValidatorService>(service =>
                {
                    helper.Validate(model => model.Code).Required(true).IF(service.CheckCourseCodeExists(Id, Code)).ErrorMsg(string.Format("Course \"{0}\" is already exists!", Code));
                }));
            }

            if (helper.Failed)
            {
                foreach (var error in helper.Errors)
                    yield return error;
            }
        }
    }
}