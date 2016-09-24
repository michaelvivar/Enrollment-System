using System;

namespace UI.Helpers.Validators
{
    public interface IValidator
    {

    }

    public interface IValidator<TValidator>
    {
        TValidator Required(bool required);
        TValidator IF(bool expression);
    }

    public abstract class Validator<T1, T2, T3, T4> where T1 : IValidator where T2 : IValidator
    {
        protected IValidationResultHelper<T3> _helper { get; set; }
        protected IModelProperty<T4> _property { get; set; }
        protected bool IsRequired { get; set; }
        protected bool Failed = false;

        public T1 ErrorMsg(string message)
        {
            if (Failed)
            {
                ((ValidationResultHelper<T3>)_helper).AddError(message, _property.Name);
                Failed = false;
            }
            return (T1)Instance();
        }
        public T2 Required(bool required)
        {
            IsRequired = required;
            if (IsRequired && _property.Value == null)
                Failed = true;

            return (T2)Instance();
        }
        public T2 GreaterThan(int num)
        {
            if (!Failed)
                if ((_property.Value == null) ? IsRequired : Convert.ToInt32(_property.Value) <= num)
                    Failed = true;

            return (T2)Instance();
        }
        public T2 IF(bool expression)
        {
            if (!Failed)
                if ((_property.Value == null) ? IsRequired : expression)
                    Failed = true;

            return (T2)Instance();
        }
        protected abstract IValidator Instance();
    }
}