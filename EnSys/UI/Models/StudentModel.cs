﻿using BL;
using BL.Dto;
using BL.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UI.Helpers;
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
        public string ContactInfo { get; set; }
    }

    public class ValidateStudentModel : StudentModel, IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            IValidationResultHelper<StudentModel> helper = new ValidationResultHelper<StudentModel>(this);

            helper.Validate(model => model.CourseId).Required(true).GreaterThan(0).ErrorMsg("Course field is required");

            helper.Validate(model => model.Level).Required(true).GreaterThan(0).ErrorMsg("Level field is required");

            helper.Validate(model => model.SectionId).Required(true).GreaterThan(0).ErrorMsg("Section field is required");

            helper.Validate(model => model.FirstName).Required(true).NotEmpty().ErrorMsg("First Name field is required")
                .MaxLength(20).ErrorMsg("First Name is too long").MinLength(2).ErrorMsg("First Name is too short");

            helper.Validate(model => model.LastName).Required(true).NotEmpty().ErrorMsg("Last Name field is required");

            helper.Validate(model => model.BirthDate).Required(true).LessThan(DateTime.Now).ErrorMsg("Invalid date of birth");

            helper.Validate(model => model.Gender).Required(true).GreaterThan(0).ErrorMsg("Gender field is required");

            helper.Validate(model => model.Status).Required(true).GreaterThan(0).ErrorMsg("Status field is required");

            helper.Validate(model => model.Email).Required(false).EmailAddress().ErrorMsg("Invalid email address");

            helper.IF(string.IsNullOrEmpty(Email) && string.IsNullOrEmpty(Telephone) && string.IsNullOrEmpty(Mobile)).ErrorMsg("Please fill atleast one of the contact information");

            if (!helper.Failed)
            {
                Transaction.Scope(scope => scope.Service<ValidatorService>(service =>
                {
                    helper.Validate(model => model.FirstName).Required(true).IF(service.CheckPersonExists(Id, FirstName, LastName, BirthDate))
                        .ErrorMsg(string.Format("Person with name \"{0} {1}\" and birth date \"{2}\" is already exists!", FirstName, LastName, ((DateTime)BirthDate).ToShortDateString()));

                    helper.Validate(model => model.Email).Required(false).IF(service.CheckEmailExists(ContactInfoId, Email))
                        .ErrorMsg(string.Format("Email address \"{0}\" is already exists!", Email));
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