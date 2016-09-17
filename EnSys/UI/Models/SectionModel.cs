﻿using BL.Dto;
using BL.Interfaces;
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
        [Required]
        public string Code { get; set; }
        public string Remarks { get; set; }
        [Required]
        public Status Status { get; set; }
        public int StatusId { get { return Convert.ToInt32(Status); } }
        [Required]
        public YearLevel Level { get; set; }
        public int LevelId { get { return Convert.ToInt32(Level); } }
    }

    public class ValidateSectionModel : SectionModel, IValidatableObject
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