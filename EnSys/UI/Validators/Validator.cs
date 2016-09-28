using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UI.Helpers;

namespace UI.Validators
{
    public abstract class Validator<TModel> : IValidator
    {
        protected TModel Model { get; set; }
        protected IValidationResultHelper<TModel> _helper { get; set; }

        public List<KeyValuePair<string, string>> Errors()
        {
            return _helper.ErrorMessages;
        }

        public void Init(object value)
        {
            Model = (TModel)value;
            _helper = new ValidationResultHelper<TModel>(Model);
        }

        public abstract bool Validate();
    }

    public interface IValidator
    {
        void Init(object value);
        bool Validate();
        List<KeyValuePair<string, string>> Errors();
    }
}