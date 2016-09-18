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
    public class CourseModel :  ICourse
    {
        public int Id { get; set; }
        [Required]
        public string Code { get; set; }
        public string Remarks { get; set; }
        [Required]
        public Status Status { get; set; }
        public int StatusId { get { return Convert.ToInt32(Status); } }
        public int Students { get; set; }
        public IEnumerable<ISubject> Subjects { get; set; }
    }

    public class ValidateCourseModel : CourseModel, IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            bool[] result = new bool[1];
            result[0] = false;

            if (result[0])
                yield return new ValidationResult("");
        }
    }
}