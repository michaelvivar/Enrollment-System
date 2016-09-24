using System;
using System.Text.RegularExpressions;

namespace UI.Helpers.Validators
{
    public class StringValidator<TModel> : Validator<IString_Validator, IStringValidator, TModel, string>, IStringValidator, IValidator<IStringValidator>
    {
        public StringValidator(IValidationResultHelper<TModel> helper, IModelProperty<string> property)
        {
            _helper = helper;
            _property = property;
        }

        public IStringValidator EmailAddress()
        {
            if (!Failed)
                if ((_property.Value == null) ? IsRequired : !(ValidateEmailAddress(_property.Value)))
                    Failed = true;

            return this;
        }

        private bool ValidateEmailAddress(string email)
        {
            return Regex.IsMatch(email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
        }

        public IStringValidator NotEmpty()
        {
            if (!Failed)
                if ((_property.Value == null) ? IsRequired : (string.IsNullOrWhiteSpace(_property.Value) || string.IsNullOrEmpty(_property.Value)))
                    Failed = true;

            return this;
        }

        public IStringValidator MinLength(int length)
        {
            if (!Failed)
                if ((_property.Value == null) ? IsRequired : _property.Value.Length < length)
                    Failed = true;

            return this;
        }

        public IStringValidator MaxLength(int length)
        {
            if (!Failed)
                if ((_property.Value == null) ? IsRequired : _property.Value.Length > length)
                    Failed = true;

            return this;
        }

        protected override IValidator Instance()
        {
            return this;
        }
    }



    public interface IString_Validator : IValidator
    {
        IStringValidator NotEmpty();
        IStringValidator EmailAddress();
        IStringValidator MinLength(int length);
        IStringValidator MaxLength(int length);
        IStringValidator IF(bool expression);
    }

    public interface IStringValidator : IString_Validator
    {
        IString_Validator ErrorMsg(string message);
    }
}