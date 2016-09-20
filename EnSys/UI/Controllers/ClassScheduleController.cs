using BL;
using BL.Dto;
using BL.Services;
using Nelibur.ObjectMapper;
using System;
using System.Linq;
using System.Web.Mvc;
using UI.Models;

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

        public ActionResult FilterData(int? day, int? instructor, int? subject, int? section, int? room)
        {
            return Transaction.Scope(scope => scope.Service<ClassScheduleService, ActionResult>(service =>
            {
                return PartialView("Table", service.GetClassesFiltered(
                    Convert.ToInt32(day), Convert.ToInt32(instructor),
                    Convert.ToInt32(subject), Convert.ToInt32(section),
                    Convert.ToInt32(room)).Select(o => MapDtoToModel(o)));
            }));

        }
    }
}