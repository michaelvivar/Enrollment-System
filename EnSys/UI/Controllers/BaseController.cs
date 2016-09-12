using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BL.Services;
using BL;

namespace UI.Controllers
{
    public abstract class BaseController : Controller
    {
        protected void Service<TService>(Action<TService> action) where TService : IService
        {
            Transaction.Service(action);
        }
        protected TOut Service<TService, TOut>(Func<TService, TOut> action) where TService : IService
        {
            return Transaction.Service(action);
        }
    }
}