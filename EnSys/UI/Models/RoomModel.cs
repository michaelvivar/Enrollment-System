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
            IValidationResultHelper<RoomModel> helper = new ValidationResultHelper<RoomModel>(this);

            helper.Validate(model => model.Number).Required(true).ErrorMsg("Number field is required");

            helper.Validate(model => model.Capacity).Required(true).GreaterThan(0).ErrorMsg("Capacity must be greater than 0(Zero)");

            helper.Validate(model => model.Status).Required(true).GreaterThan(0).ErrorMsg("Status field is required");

            if (helper.Errors.Count == 0)
            {
                Transaction.Scope(scope => scope.Service<RoomValidatorService>(service =>
                {
                    helper.Validate(model => model.Number).Required(true).IF(service.CheckRoomNumberExists(Id, Number)).ErrorMsg(string.Format("Room number/name \"{0}\" is already exists!", Number));
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