using System;

namespace UI.Helpers.Validators
{
    public interface IValidator_Required<TValidator>
    {
        TValidator Required(bool required);
        TValidator IF(bool expression);
    }

    public interface IValidator<T1, T2>
    {
        T1 ErrorMsg(string message);
        T2 Required(bool required);
    }

    public class Validator<T1, T2, T3, T4> : IValidator<T1, T2>
    {
        protected IValidationResultHelper<T3> _helper { get; set; }
        protected IModelProperty<T4> _property { get; set; }
        protected bool IsRequired { get; set; }
        protected bool Failed = false;
        protected T1 Instance1 { get; set; }
        protected T2 Instance2 { get; set; }
        public T1 ErrorMsg(string message)
        {
            if (Failed)
            {
                ((ValidationResultHelper<T3>)_helper).AddError(message, _property.Name);
                Failed = false;
            }
            return Instance1;
        }
        public T2 Required(bool required)
        {
            IsRequired = required;
            if (IsRequired && _property.Value == null)
                Failed = true;

            return Instance2;
        }
        public T2 GreaterThan(int num)
        {
            if (!Failed)
                if ((_property.Value == null) ? IsRequired : Convert.ToInt32(_property.Value) <= num)
                    Failed = true;

            return Instance2;
        }
        public T2 IF(bool expression)
        {
            if (!Failed)
                if (IsRequired ? false : expression)
                    Failed = true;

            return Instance2;
        }
    }
}