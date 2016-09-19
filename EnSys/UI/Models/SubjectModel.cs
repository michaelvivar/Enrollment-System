using BL;
using BL.Dto;
using BL.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Util.Enums;

namespace UI.Models
{
    public class SubjectModel : ISubject
    {
        public int Id { get; set; }
        public string Code { get; set; }
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
            bool hasError = false;

            if (string.IsNullOrWhiteSpace(Code) || string.IsNullOrEmpty(Code))
            {
                hasError = true;
                yield return new ValidationResult("Code field is required", new[] { nameof(Code) });
            }

            if (Level == null || Level <= 0)
            {
                hasError = true;
                yield return new ValidationResult("Year level field is required", new[] { nameof(Level) });
            }

            if (Units == null || Units <= 0)
            {
                hasError = true;
                yield return new ValidationResult("Units field is required", new[] { nameof(Units) });
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
                    scope.Service<SubjectValidatorService>(service =>
                    {
                        if (service.CheckSubjectCodeExists(Id, Code))
                            result.Add(new ValidationResult(string.Format("Subject \"{0}\" is already exists!", Code), new[] { nameof(Code) }));
                    });
                });

                if (result.Count > 0)
                    foreach (var i in result)
                        yield return i;
            }

        }
    }
}