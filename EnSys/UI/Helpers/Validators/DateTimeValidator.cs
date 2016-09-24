using System;
using System.Data.SqlTypes;

namespace UI.Helpers.Validators
{
    public class DateTimeValidator<TModel> : Validator<IDateTime_Validator, IDateTimeValidator, TModel, DateTime?>, IDateTimeValidator, IValidator<IDateTimeValidator>
    {
        public DateTimeValidator(ValidationResultHelper<TModel> helper, IModelProperty<DateTime?> property)
        {
            _helper = helper;
            _property = property;
            if (_property.Value < (DateTime)SqlDateTime.MinValue)
                Failed = true;
        }

        public IDateTimeValidator GreaterThan(DateTime date)
        {
            if (!Failed)
                if ((_property.Value == null) ? IsRequired : _property.Value <= date)
                    Failed = true;

            return this;
        }

        public IDateTimeValidator LessThan(DateTime date)
        {
            if (!Failed)
                if ((_property.Value == null) ? IsRequired : _property.Value >= date)
                    Failed = true;

            return this;
        }

        protected override IValidator Instance()
        {
            return this;
        }
    }

    public interface IDateTime_Validator : IValidator
    {
        IDateTimeValidator GreaterThan(DateTime date);
        IDateTimeValidator LessThan(DateTime date);
        IDateTimeValidator IF(bool expression);
    }

    public interface IDateTimeValidator : IDateTime_Validator
    {
        IDateTime_Validator ErrorMsg(string message);
    }
}