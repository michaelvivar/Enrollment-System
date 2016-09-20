using System.Text.RegularExpressions;

namespace UI.Helpers.Validators
{
    public class StringValidator<TModel> : Validator<IStringValidator1, IStringValidator2, TModel, string>, IStringValidator1, IStringValidator2, IValidator_Required<IStringValidator2>
    {
        public StringValidator(IValidationResultHelper<TModel> helper, IModelProperty<string> property)
        {
            Instance1 = this;
            Instance2 = this;
            _helper = helper;
            _property = property;
        }

        public IStringValidator2 EmailAddress()
        {
            if (!Failed)
                if ((_property.Value == null) ? IsRequired : !(ValidateEmailAddress(_property.Value)))
                    Failed = true;

            return Instance2;
        }

        private bool ValidateEmailAddress(string email)
        {
            return Regex.IsMatch(email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
        }

        public IStringValidator2 NotEmpty()
        {
            if (!Failed)
                if ((_property.Value == null) ? IsRequired : (string.IsNullOrWhiteSpace(_property.Value) || string.IsNullOrEmpty(_property.Value)))
                    Failed = true;

            return Instance2;
        }

        public IStringValidator2 MinLength(int length)
        {
            if (!Failed)
                if ((_property.Value == null) ? IsRequired : _property.Value.Length < length)
                    Failed = true;

            return Instance2;
        }

        public IStringValidator2 MaxLength(int length)
        {
            if (!Failed)
                if ((_property.Value == null) ? IsRequired : _property.Value.Length > length)
                    Failed = true;

            return Instance2;
        }
    }

    public interface IStringValidator1
    {
        IStringValidator2 NotEmpty();
        IStringValidator2 EmailAddress();
        IStringValidator2 MinLength(int length);
        IStringValidator2 MaxLength(int length);
        IStringValidator2 IF(bool expression);
    }

    public interface IStringValidator2 : IStringValidator1
    {
        IStringValidator1 ErrorMsg(string message);
    }
}