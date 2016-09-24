using System;

namespace UI.Helpers.Validators
{
    public class TypeValidator<TModel, T> : Validator<IType_Validator, ITypeValidator, TModel, T>, ITypeValidator, IValidator<ITypeValidator>
    {
        public TypeValidator(ValidationResultHelper<TModel> helper, IModelProperty<T> property)
        {
            _helper = helper;
            _property = property;
        }

        protected override IValidator Instance()
        {
            return this;
        }
    }

    public interface IType_Validator : IValidator
    {
        ITypeValidator GreaterThan(int num);
        ITypeValidator IF(bool expression);
    }

    public interface ITypeValidator : IType_Validator
    {
        IType_Validator ErrorMsg(string message);
    }


    public class ConditionValidator : IConditionValidator
    {
        private ValidationResultHelper _helper { get; set; }
        private bool Failed = false;
        public ConditionValidator(ValidationResultHelper helper)
        {
            _helper = helper;
        }

        public IConditionValidator IF(bool expression)
        {
            if (expression)
                Failed = true;

            return this;
        }

        public void ErrorMsg(string message)
        {
            if (Failed)
                _helper.AddError(message, "validation-summary-top");
        }
    }

    public interface IConditionValidator
    {
        void ErrorMsg(string message);
    }
}