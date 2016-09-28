using BL;
using BL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UI.Models;

namespace UI.Validators
{
    public class RoomModelValidator : Validator<RoomModel>
    {
        public override bool Validate()
        {
            _helper.Validate(model => model.Number).Required(true).NotEmpty().ErrorMsg("Number field is required");

            _helper.Validate(model => model.Capacity).Required(true).GreaterThan(0).ErrorMsg("Capacity must be greater than 0(Zero)");

            _helper.Validate(model => model.Status).Required(true).GreaterThan(0).ErrorMsg("Status field is required");

            if (!_helper.Failed)
            {
                Transaction.Scope(scope => scope.Service<RoomValidatorService>(service =>
                {
                    _helper.Validate(model => model.Number).Required(true).IF(service.CheckRoomNumberExists(Model.Id, Model.Number)).ErrorMsg(string.Format("Room number/name \"{0}\" is already exists!", Model.Number));
                }));
            }

            return !_helper.Failed;
        }
    }
}