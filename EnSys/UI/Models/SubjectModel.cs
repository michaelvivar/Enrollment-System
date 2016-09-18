using BL.Dto;
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
            bool[] result = new bool[1];
            result[0] = false;

            if (result[0])
                yield return new ValidationResult("");
        }
    }
}