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
    public class SubjectModel : ISubject
    {
        public int Id { get; set; }
        public string Code { get; set; }
        [Display(Name = "No. of Units")]
        public Unit? Units { get; set; }
        public int UnitId { get { return Convert.ToInt32(Units); } }
        public string Remarks { get; set; }
        public Status? Status { get; set; }
        public int StatusId { get { return Convert.ToInt32(Status); } }
        public YearLevel? Level { get; set; }
        public int LevelId { get { return Convert.ToInt32(Level); } }
    }

    public class ValidateSubjectModel : SubjectModel, IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            IValidationResultHelper<SubjectModel> helper = new ValidationResultHelper<SubjectModel>(this);

            helper.Validate(model => model.Code).Required(true).ErrorMsg("Code field is required");

            helper.Validate(model => model.Level).Required(true).GreaterThan(0).ErrorMsg("Year level field is required");

            helper.Validate(model => model.Units).Required(true).GreaterThan(0).ErrorMsg("Units field is required");

            helper.Validate(model => model.Status).Required(true).GreaterThan(0).ErrorMsg("Status field is required");

            if (!helper.Failed)
            {
                Transaction.Scope(scope => scope.Service<SubjectValidatorService>(service =>
                {
                    helper.Validate(model => model.Code).Required(true).IF(service.CheckSubjectCodeExists(Id, Code)).ErrorMsg(string.Format("Subject \"{0}\" is already exists!", Code));
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