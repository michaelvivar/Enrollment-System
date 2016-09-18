using BL;
using BL.Dto;
using BL.Interfaces;
using BL.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
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
        public int Students { get; set; }
    }

    public class ValidateSectionModel : SectionModel, IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            bool hasError = false;

            if (string.IsNullOrWhiteSpace(Code) || string.IsNullOrEmpty(Code))
            {
                hasError = true;
                yield return new ValidationResult("Number field is required", new[] { nameof(Code) });
            }

            if (Level == null || Level <= 0)
            {
                hasError = true;
                yield return new ValidationResult("Year level field is required", new[] { nameof(Level) });
            }

            if (Status == null || Status <= 0)
            {
                hasError = true;
                yield return new ValidationResult("Status field is required", new[] { nameof(Status) });
            }

            if (!hasError)
            {
                List<ValidationResult> result = new List<ValidationResult>();
                Transaction.Scope(scope =>
                {
                    scope.Service<SectionValidatorService>(service =>
                    {
                        if (service.CheckSecionCodeExists(Id, Code))
                            result.Add(new ValidationResult(string.Format("Room number/name \"{0}\" is already exists!", Code), new[] { nameof(Code) }));
                    });
                });

                if (result.Count > 0)
                    foreach (var i in result)
                        yield return i;
            }

        }
    }
}