using BL;
using BL.Dto;
using BL.Interfaces;
using BL.Services;
using Nelibur.ObjectMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UI.Models;
using Util.Enums;

namespace UI.Controllers
{
    public class ClassScheduleController : BaseController
    {
        private ClassScheduleModel MapDtoToModel(IClassSchedule dto)
        {
            return TinyMapper.Map<ClassScheduleModel>(dto);
        }

        [Route("")]
        public ActionResult Index()
        {
            return Transaction.Scope(scope => scope.Service<ClassScheduleService, ActionResult>(service => View(service.GetClasses().Select(o => MapDtoToModel(o)))));
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(new ClassScheduleModel() { TimeStart = DateTime.Now, TimeEnd = DateTime.Now });
        }

        [HttpPost]
        public ActionResult Create(ValidateClassScheduleModel model)
        {
            if (ModelState.IsValid)
            {
                Transaction.Scope(scope => scope.Service<ClassScheduleService>(service => service.AddClassSchedule(model)));
                return JsonUrlSuccess(Url.Action("Index"));
            }
            return JsonFormError(ModelState);
        }

        public ActionResult Edit(int id)
        {
            return Transaction.Scope(scope => scope.Service<ClassScheduleService, ActionResult>(service => View(MapDtoToModel(service.GetClass(id)))));
        }

        [HttpPost]
        public ActionResult Edit(ValidateClassScheduleModel model)
        {
            if (ModelState.IsValid)
            {
                Transaction.Scope(scope => scope.Service<ClassScheduleService>(service => service.UpdateClassSchedule(model)));
                return JsonUrlSuccess(Url.Action("Index"));
            }
            return JsonFormError(ModelState);
        }
    }
}