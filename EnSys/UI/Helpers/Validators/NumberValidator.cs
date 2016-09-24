using System;

namespace UI.Helpers.Validators
{
    public class NumberValidator<TModel> : Validator<INumber_Validator, INumberValidator, TModel, int?>, INumberValidator, IValidator<INumberValidator>
    {
        public NumberValidator(ValidationResultHelper<TModel> helper, IModelProperty<int?> property)
        {
            _helper = helper;
            _property = property;
        }

        protected override IValidator Instance()
        {
            return this;
        }
    }

    public interface INumber_Validator : IValidator
    {
        INumberValidator GreaterThan(int num);
        INumberValidator IF(bool expression);
    }

    public interface INumberValidator : INumber_Validator
    {
        INumber_Validator ErrorMsg(string message);
    }
}