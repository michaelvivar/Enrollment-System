using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace UI.Helpers
{
    public interface IValidationResultHelper<TModel>
    {
        IValidator_Required<IStringValidator2> Validate(Expression<Func<TModel, string>> property);
        IValidator_Required<IDateTimeValidator2> Validate(Expression<Func<TModel, DateTime?>> property);
        IValidator_Required<INumberValidator2> Validate(Expression<Func<TModel, int?>> property);
        IValidator_Required<ITypeValidator2> Validate<T>(Expression<Func<TModel, T>> property);
        IConditionValidator IF(bool expression);

        List<ValidationResult> Errors { get; set; }
        bool Failed { get; set; }
    }

    public class ValidationResultHelper
    {
        public bool Failed { get; set; }
        public List<ValidationResult> Errors { get; set; }

        public void AddError(string message, string property)
        {
            Errors.Add(new ValidationResult(message, new[] { property }));
        }
    }

    public class ValidationResultHelper<TModel> : ValidationResultHelper, IValidationResultHelper<TModel>
    {
        private readonly TModel Model;
        public ValidationResultHelper(TModel model) { Model = model; Errors = new List<ValidationResult>(); }

        private string GetPropertyName<T>(Expression<Func<TModel, T>> expression)
        {
            if (expression.Body is MemberExpression)
            {
                return ((MemberExpression)expression.Body).Member.Name;
            }
            else
            {
                var op = ((UnaryExpression)expression.Body).Operand;
                return ((MemberExpression)op).Member.Name;
            }
        }
        private object GetPropertyValue(string property)
        {
            Type type = typeof(TModel);
            return type.GetProperty(property).GetValue(Model, null);
        }
        private T GetPropertyValue<T>(string property)
        {
            Type type = typeof(TModel);
            object value = type.GetProperty(property).GetValue(Model, null);

            var t = typeof(T);
            if (t.IsGenericType && t.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                if (value == null)
                    return default(T);

                t = Nullable.GetUnderlyingType(t);
            }

            return (T)Convert.ChangeType(value, t);
        }

        public void AddError(string message, Expression<Func<TModel, object>> property)
        {
            AddError(message, GetPropertyName(property));
        }

        public IConditionValidator IF(bool expression)
        {
            return new ConditionValidator(this).IF(expression);
        }
        public IValidator_Required<IStringValidator2> Validate(Expression<Func<TModel, string>> property)
        {
            string name = GetPropertyName(property);
            string value = GetPropertyValue<string>(name);
            return new StringValidator<TModel>(this, new ModelProperty<string> { Name = name, Value = value });
        }
        public IValidator_Required<INumberValidator2> Validate(Expression<Func<TModel, int?>> property)
        {
            string name = GetPropertyName(property);
            int? value = GetPropertyValue<int?>(name);
            return new NumberValidator<TModel>(this, new ModelProperty<int?> { Name = name, Value = value });
        }
        public IValidator_Required<IDateTimeValidator2> Validate(Expression<Func<TModel, DateTime?>> property)
        {
            string name = GetPropertyName(property);
            DateTime? value = GetPropertyValue<DateTime?>(name);
            return new DateTimeValidator<TModel>(this, new ModelProperty<DateTime?> { Name = name, Value = value });
        }
        public IValidator_Required<ITypeValidator2> Validate<T>(Expression<Func<TModel, T>> property)
        {
            string name = GetPropertyName(property);
            T value = GetPropertyValue<T>(name);
            return new TypeValidator<TModel, T>(this, new ModelProperty<T> { Name = name, Value = value });
        }
    }

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
                if ((_property.Value == null) ? IsRequired :Convert.ToInt32(_property.Value) <= num)
                {
                    _helper.Failed = true; Failed = true;
                }

            return Instance2;
        }
        public T2 IF(bool expression)
        {
            if (!Failed)
                if (IsRequired ? false : expression)
                {
                    _helper.Failed = true; Failed = true;
                }

            return Instance2;
        }
    }

    public class StringValidator<TModel> : Validator<IStringValidator1, IStringValidator2, TModel, string>, IStringValidator1, IStringValidator2, IValidator_Required<IStringValidator2>
    {
        public StringValidator(IValidationResultHelper<TModel> helper, IModelProperty<string> property)
        {
            Instance1 = this;
            Instance2 = this;
            _helper = helper;
            _property = property;
            if (IsRequired && _property.Value == null)
                Failed = true;
        }

        public IStringValidator2 EmailAddress()
        {
            if (!Failed)
                if ((_property.Value == null) ? IsRequired : !(ValidateEmailAddress(_property.Value)))
                {
                    _helper.Failed = true; Failed = true;
                }

            return Instance2;
        }

        private bool ValidateEmailAddress(string email)
        {
            return Regex.IsMatch(email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
        }

        public IStringValidator2 NotEmpty()
        {
            if (!Failed)
                if ((_property.Value == null) ? IsRequired : string.IsNullOrEmpty(_property.Value))
                {
                    _helper.Failed = true; Failed = true;
                }

            return Instance2;
        }
    }

    public class NumberValidator<TModel> : Validator<INumberValidator1, INumberValidator2, TModel, int?>, INumberValidator1, INumberValidator2, IValidator_Required<INumberValidator2>
    {
        public NumberValidator(ValidationResultHelper<TModel> helper, IModelProperty<int?> property)
        {
            Instance1 = this;
            Instance2 = this;
            _helper = helper;
            _property = property;
            if (IsRequired && _property.Value == null)
                Failed = true;
        }
    }

    public class DateTimeValidator<TModel> : Validator<IDateTimeValidator1, IDateTimeValidator2, TModel, DateTime?>, IDateTimeValidator1, IDateTimeValidator2, IValidator_Required<IDateTimeValidator2>
    {
        public DateTimeValidator(ValidationResultHelper<TModel> helper, IModelProperty<DateTime?> property)
        {
            Instance1 = this;
            Instance2 = this;
            _helper = helper;
            _property = property;
            if (IsRequired && ((_property.Value == null) ? true : (_property.Value < (DateTime)SqlDateTime.MinValue)))
                Failed = true;
        }

        public IDateTimeValidator2 GreaterThan(DateTime date)
        {
            if (!Failed)
                if ((_property.Value == null) ? IsRequired : _property.Value <= date)
                {
                    _helper.Failed = true; Failed = true;
                }

            return Instance2;
        }

        public IDateTimeValidator2 LessThan(DateTime date)
        {
            if (!Failed)
                if ((_property.Value == null) ? IsRequired : _property.Value >= date)
                {
                    _helper.Failed = true; Failed = true;
                }

            return Instance2;
        }
    }

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
            {
                Failed = true;
                _helper.Failed = true;
            }

            return this;
        }

        public void ErrorMsg(string message)
        {
            if (Failed)
            {
                _helper.AddError(message, "validation-summary-top");
            }
        }
    }

    public interface IStringValidator1
    {
        IStringValidator2 NotEmpty();
        IStringValidator2 EmailAddress();
        IStringValidator2 IF(bool expression);
    }

    public interface IStringValidator2 : IStringValidator1
    {
        IStringValidator1 ErrorMsg(string message);
    }

    public interface INumberValidator1
    {
        INumberValidator2 GreaterThan(int num);
        INumberValidator2 IF(bool expression);
    }

    public interface INumberValidator2 : INumberValidator1
    {
        INumberValidator1 ErrorMsg(string message);
    }

    public interface IDateTimeValidator1
    {
        IDateTimeValidator2 GreaterThan(DateTime date);
        IDateTimeValidator2 LessThan(DateTime date);
    }

    public interface IDateTimeValidator2 : IDateTimeValidator1
    {
        IDateTimeValidator1 ErrorMsg(string message);
    }

    public interface ITypeValidator1
    {
        ITypeValidator2 GreaterThan(int num);
    }

    public interface IConditionValidator
    {
        void ErrorMsg(string message);
    }

    public interface ITypeValidator2 : ITypeValidator1
    {
        ITypeValidator1 ErrorMsg(string message);
    }

    public interface IModelProperty<T>
    {
        string Name { get; set; }
        T Value { get; set; }
    }

    public class ModelProperty<T> : IModelProperty<T>
    {
        public string Name { get; set; }
        public T Value { get; set; }
    }
}