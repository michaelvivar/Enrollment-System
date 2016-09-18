using BL;
using BL.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UI.Models
{
    public class ClassScheduleModel : IClassSchedule
    {
        public int Id { get; set; }
        [Display(Name = "Day of the Week")]
        public DayOfWeek? Day { get; set; }
        public int DayId { get { return (Day.HasValue ? (int)Day : 0); } }
        [Display(Name = "Day(s) of the Week")]
        public int?[] Days { get; set; }
        [Display(Name = "Time Start")] [DataType(DataType.Time)]
        public DateTime? TimeStart { get; set; }
        [Display(Name = "Time End")] [DataType(DataType.Time)]
        public DateTime? TimeEnd { get; set; }
        public int? Capacity { get; set; }
        public string Remarks { get; set; }
        [Display(Name = "Instructor")]
        public int? InstructorId { get; set; }
        public string InstructorFirstName { get; set; }
        public string InstructorLastName { get; set; }
        [Display(Name = "Subject")]
        public int? SubjectId { get; set; }
        public string Subject { get; set; }
        [Display(Name = "Section")]
        public int? SectionId { get; set; }
        public string Section { get; set; }
        [Display(Name = "Room")]
        public int? RoomId { get; set; }
        public string Room { get; set; }
    }

    public class ValidateClassScheduleModel : ClassScheduleModel, IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            bool hasError = false;

            if (!TimeStart.HasValue)
            {
                hasError = true;
                yield return new ValidationResult("Time Start field is required", new[] { nameof(TimeStart) });
            }

            if (!TimeEnd.HasValue)
            {
                hasError = true;
                yield return new ValidationResult("Time End field is required", new[] { nameof(TimeEnd) });
            }

            if (TimeStart.HasValue && TimeEnd.HasValue && TimeStart >= TimeEnd)
            {
                hasError = true;
                yield return new ValidationResult("Time End field must be greater than Time Start", new[] { nameof(TimeEnd) });
            }

            if (Days == null || Days.Length <= 0 || (Days.Length == 1 && Days[0] == null))
            {
                hasError = true;
                yield return new ValidationResult("Day(s) of the Week field is required", new[] { nameof(DayId) });
            }

            if ((!RoomId.HasValue) || RoomId <= 0)
            {
                hasError = true;
                yield return new ValidationResult("Room field is required", new[] { nameof(RoomId) });
            }

            if ((!SectionId.HasValue) || RoomId <= 0)
            {
                hasError = true;
                yield return new ValidationResult("Section field is required", new[] { nameof(SectionId) });
            }
                        if ((!SubjectId.HasValue) || RoomId <= 0)
            {
                hasError = true;
                yield return new ValidationResult("Subject field is required", new[] { nameof(SubjectId) });
            }

            if ((!InstructorId.HasValue) || RoomId <= 0)
            {
                hasError = true;
                yield return new ValidationResult("Instructor field is required", new[] { nameof(InstructorId) });
            }

            if ((!Capacity.HasValue) || Capacity <= 0)
            {
                hasError = true;
                yield return new ValidationResult("Capacity field is required and must be greater than to 0(zero)", new[] { nameof(Capacity) });
            }

            if (!hasError)
            {
                List<ValidationResult> result = new List<ValidationResult>();
                Transaction.Scope(scope =>
                {

                });

                foreach (var i in result)
                    yield return i;
            }
        }
    }
}