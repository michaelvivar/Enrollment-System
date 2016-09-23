using BL;
using BL.Dto;
using BL.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UI.Helpers;

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
            if (DayId != 0)
                Days[0] = DayId;

            IValidationResultHelper<ClassScheduleModel> helper = new ValidationResultHelper<ClassScheduleModel>(this);

            helper.Validate(model => model.TimeStart).Required(true).ErrorMsg("Time Start field is required");

            helper.Validate(model => model.TimeEnd).Required(true).ErrorMsg("Time End field is required")
                .IF(TimeStart >= TimeEnd).ErrorMsg("Time End field must be greater than Time Start");

            helper.Validate(model => model.DayId).Required(true).IF(Days == null || Days.Length <= 0 || (Days.Length == 1 && Days[0] == null)).ErrorMsg("Status field is required");

            helper.Validate(model => model.RoomId).Required(true).GreaterThan(0).ErrorMsg("Room field is required");

            helper.Validate(model => model.SectionId).Required(true).GreaterThan(0).ErrorMsg("Section field is required");

            helper.Validate(model => model.SubjectId).Required(true).GreaterThan(0).ErrorMsg("Subject field is required");

            helper.Validate(model => model.InstructorId).Required(true).GreaterThan(0).ErrorMsg("Instructor field is required");

            helper.Validate(model => model.Capacity).Required(true).GreaterThan(0).ErrorMsg("Capacity field is required and must be greater than to 0(Zero)");

            if (!helper.Failed)
            {
                Transaction.Scope(scope =>
                {
                    foreach (DayOfWeek day in Days)
                    {
                        helper.Validate(model => model.RoomId).IF(!scope.Service<RoomValidatorService, bool>(service => service.CheckRoomAvailavility(
                            Id, RoomId, (DateTime)TimeStart, (DateTime)TimeEnd, day)))
                            .ErrorMsg(string.Format("Room is not available, between {0} to {1}",
                                    ((DateTime)TimeStart).ToShortTimeString(),
                                    ((DateTime)TimeEnd).ToShortTimeString())
                            );

                        helper.Validate(model => model.SectionId).IF(!scope.Service<SectionValidatorService, bool>(service => service.CheckSectionAvailability(
                            Id, SectionId, (DateTime)TimeStart, (DateTime)TimeEnd, day)))
                            .ErrorMsg(string.Format("Section is not available, between {0} to {1}",
                                    ((DateTime)TimeStart).ToShortTimeString(),
                                    ((DateTime)TimeEnd).ToShortTimeString())
                            );

                        helper.Validate(model => model.InstructorId).IF(!scope.Service<InstructorValidatorService, bool>(service => service.CheckInstructorAvailability(
                            Id, InstructorId, (DateTime)TimeStart, (DateTime)TimeEnd, day)))
                            .ErrorMsg(string.Format("Instructor is not available, between {0} to {1}",
                                    ((DateTime)TimeStart).ToShortTimeString(),
                                    ((DateTime)TimeEnd).ToShortTimeString())
                            );

                        if (helper.Failed)
                            break;
                    }
                });
            }

            if (helper.Failed)
            {
                foreach (var error in helper.Errors)
                    yield return error;
            }
        }
    }
}