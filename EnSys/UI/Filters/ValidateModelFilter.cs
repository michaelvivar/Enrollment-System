using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using UI.Validators;
using Util.Enums;

namespace UI.Filters
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public Type Validator { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.Result = new HttpStatusCodeResult(404);
            }

            filterContext.Controller.ViewData.ModelState.Clear();

            var model = filterContext.ActionParameters.FirstOrDefault().Value;
            IValidator validator = (IValidator)Activator.CreateInstance(Validator);
            validator.Init(model);

            if (!validator.Validate())
            {
                filterContext.Result = new JsonResult()
                {
                    Data = new
                    {
                        Status = ActionResultStatus.Error,
                        Messages = validator.Errors().Select(o => o.Value),
                        Data = validator.Errors().Select(o => new { Field = o.Key, Errors = new[] { o.Value } })
                    },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
        }
    }
}