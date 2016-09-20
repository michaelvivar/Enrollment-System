namespace UI.Helpers.Validators
{
    public class TypeValidator<TModel, T> : Validator<ITypeValidator1, ITypeValidator2, TModel, T>, ITypeValidator1, ITypeValidator2, IValidator_Required<ITypeValidator2>
    {
        public TypeValidator(ValidationResultHelper<TModel> helper, IModelProperty<T> property)
        {
            Instance1 = this;
            Instance2 = this;
            _helper = helper;
            _property = property;
        }
    }

    public interface ITypeValidator1
    {
        ITypeValidator2 GreaterThan(int num);
    }

    public interface ITypeValidator2 : ITypeValidator1
    {
        ITypeValidator1 ErrorMsg(string message);
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