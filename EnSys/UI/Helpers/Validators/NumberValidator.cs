namespace UI.Helpers.Validators
{
    public class NumberValidator<TModel> : Validator<INumberValidator1, INumberValidator2, TModel, int?>, INumberValidator1, INumberValidator2, IValidator_Required<INumberValidator2>
    {
        public NumberValidator(ValidationResultHelper<TModel> helper, IModelProperty<int?> property)
        {
            Instance1 = this;
            Instance2 = this;
            _helper = helper;
            _property = property;
        }
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
}