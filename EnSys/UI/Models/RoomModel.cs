using BL;
using BL.Dto;
using BL.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Util.Enums;

namespace UI.Models
{
    public class RoomModel :  IRoom
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public int? Capacity { get; set; }
        public string Remarks { get; set; }
        public Status? Status { get; set; }
        public int StatusId { get { return Convert.ToInt32(Status); } }
    }

    public class ValidateRoomModel : RoomModel, IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            bool hasError = false;

            if (string.IsNullOrWhiteSpace(Number) || string.IsNullOrEmpty(Number))
            {
                hasError = true;
                yield return new ValidationResult("Number field is required", new[] { nameof(Number) });
            }

            if (Capacity == null || Capacity <= 0)
            {
                hasError = true;
                yield return new ValidationResult("Capacity must be greater than to 0(zero)", new[] { nameof(Capacity) });
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
                    scope.Service<RoomValidatorService>(service =>
                    {
                        if (service.CheckRoomNumberExists(Id, Number))
                            result.Add(new ValidationResult(string.Format("Room number/name \"{0}\" is already exists!", Number), new[] { nameof(Number) }));
                    });
                });

                if (result.Count > 0)
                    foreach (var i in result)
                        yield return i;
            }
                
        }
    }
}