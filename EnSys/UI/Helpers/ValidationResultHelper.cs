using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using UI.Helpers.Validators;

namespace UI.Helpers
{
    public interface IValidationResultHelper<TModel>
    {
        IValidator<IStringValidator> Validate(Expression<Func<TModel, string>> property);
        IValidator<IDateTimeValidator> Validate(Expression<Func<TModel, DateTime?>> property);
        IValidator<INumberValidator> Validate(Expression<Func<TModel, int?>> property);
        IValidator<ITypeValidator> Validate<T>(Expression<Func<TModel, T>> property);
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
            Failed = true;
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
        public IValidator<IStringValidator> Validate(Expression<Func<TModel, string>> property)
        {
            string name = GetPropertyName(property);
            string value = GetPropertyValue<string>(name);
            return new StringValidator<TModel>(this, new ModelProperty<string> { Name = name, Value = value });
        }
        public IValidator<INumberValidator> Validate(Expression<Func<TModel, int?>> property)
        {
            string name = GetPropertyName(property);
            int? value = GetPropertyValue<int?>(name);
            return new NumberValidator<TModel>(this, new ModelProperty<int?> { Name = name, Value = value });
        }
        public IValidator<IDateTimeValidator> Validate(Expression<Func<TModel, DateTime?>> property)
        {
            string name = GetPropertyName(property);
            DateTime? value = GetPropertyValue<DateTime?>(name);
            return new DateTimeValidator<TModel>(this, new ModelProperty<DateTime?> { Name = name, Value = value });
        }
        public IValidator<ITypeValidator> Validate<T>(Expression<Func<TModel, T>> property)
        {
            string name = GetPropertyName(property);
            T value = GetPropertyValue<T>(name);
            return new TypeValidator<TModel, T>(this, new ModelProperty<T> { Name = name, Value = value });
        }
    }

    public class ModelProperty<T> : IModelProperty<T>
    {
        public string Name { get; set; }
        public T Value { get; set; }
    }

    public interface IModelProperty<T>
    {
        string Name { get; set; }
        T Value { get; set; }
    }
}