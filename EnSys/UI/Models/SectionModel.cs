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
    public class SectionModel :  ISection
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Remarks { get; set; }
        public Status? Status { get; set; }
        public int StatusId { get { return Convert.ToInt32(Status); } }
        [Display(Name = "Year Level")]
        public YearLevel? Level { get; set; }
        public int LevelId { get { return Convert.ToInt32(Level); } }
        public int? Students { get; set; }
    }

    public class ValidateSectionModel : SectionModel, IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            IValidationResultHelper<SectionModel> helper = new ValidationResultHelper<SectionModel>(this);

            helper.Validate(model => model.Code).Required(true).ErrorMsg("Code field is required");

            helper.Validate(model => model.Level).Required(true).GreaterThan(0).ErrorMsg("Year level field is required");

            helper.Validate(model => model.Status).Required(true).GreaterThan(0).ErrorMsg("Status field is required");

            if (helper.Errors.Count == 0)
            {
                Transaction.Scope(scope => scope.Service<SectionValidatorService>(service =>
                {
                    helper.Validate(model => model.Code).Required(true).IF(service.CheckSecionCodeExists(Id, Code)).ErrorMsg(string.Format("Section \"{0}\" is already exists!", Code));
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