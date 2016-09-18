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
        public int DayId { get { return (int)Day; } }
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