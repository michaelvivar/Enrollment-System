using System;
using System.Data.SqlTypes;

namespace UI.Helpers.Validators
{
    public class DateTimeValidator<TModel> : Validator<IDateTimeValidator1, IDateTimeValidator2, TModel, DateTime?>, IDateTimeValidator1, IDateTimeValidator2, IValidator_Required<IDateTimeValidator2>
    {
        public DateTimeValidator(ValidationResultHelper<TModel> helper, IModelProperty<DateTime?> property)
        {
            Instance1 = this;
            Instance2 = this;
            _helper = helper;
            _property = property;
            if (_property.Value < (DateTime)SqlDateTime.MinValue)
                Failed = true;
        }

        public IDateTimeValidator2 GreaterThan(DateTime date)
        {
            if (!Failed)
                if ((_property.Value == null) ? IsRequired : _property.Value <= date)
                    Failed = true;

            return Instance2;
        }

        public IDateTimeValidator2 LessThan(DateTime date)
        {
            if (!Failed)
                if ((_property.Value == null) ? IsRequired : _property.Value >= date)
                    Failed = true;

            return Instance2;
        }
    }

    public interface IDateTimeValidator1
    {
        IDateTimeValidator2 GreaterThan(DateTime date);
        IDateTimeValidator2 LessThan(DateTime date);
        IDateTimeValidator2 IF(bool expression);
    }

    public interface IDateTimeValidator2 : IDateTimeValidator1
    {
        IDateTimeValidator1 ErrorMsg(string message);
    }
}