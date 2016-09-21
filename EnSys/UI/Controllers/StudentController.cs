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
    public class StudentController : BaseController
    {
        private StudentModel MapDtoToModel(IStudent dto)
        {
            return TinyMapper.Map<StudentModel>(dto);
        }

        private ClassScheduleModel MapScheduleDtoToModel(IClassSchedule dto)
        {
            return TinyMapper.Map<ClassScheduleModel>(dto);
        }

        [Route("")]
        public ActionResult Index()
        {
            return Transaction.Scope(scope => scope.Service<StudentService, ActionResult>(service => View(service.GetStudents().Select(o => MapDtoToModel(o)))));
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(new StudentModel());
        }

        [HttpPost]
        public ActionResult Create(ValidateStudentModel model)
        {
            if (ModelState.IsValid)
            {
                Transaction.Scope(scope => scope.Service<StudentService>(service => service.AddStudent(model)));
                return JsonUrlSuccess(Url.Action("Index"));
            }
            return JsonFormError(ModelState);
        }

        public ActionResult Edit(int id)
        {
            return Transaction.Scope(scope => scope.Service<StudentService, ActionResult>(service => View(MapDtoToModel(service.GetStudent(id)))));
        }

        [HttpPost]
        public ActionResult Edit(ValidateStudentModel model)
        {
            if (ModelState.IsValid)
            {
                Transaction.Scope(scope => scope.Service<StudentService>(service => service.UpdateStudent(model)));
                return JsonUrlSuccess(Url.Action("Index"));
            }
            return JsonFormError(ModelState);
        }

        public ActionResult Details(int id)
        {
            return Transaction.Scope(scope => scope.Service<StudentService, ActionResult>(service => View(MapDtoToModel(service.GetStudent(id)))));
        }

        public ActionResult Schedule(int id)
        {
            return Transaction.Scope(scope => scope.Service<ClassScheduleService, ActionResult>(service => PartialView("Schedule", service.GetClassesByStudentId(id).Select(o => MapScheduleDtoToModel(o)))));
        }

        public ActionResult FilterData(int? level, int? section, int? course, int? status)
        {
            return Transaction.Scope(scope => scope.Service<StudentService, ActionResult>(service =>
            {
                return PartialView("Table", service.GetStudentsFiltered(
                    Convert.ToInt32(level), Convert.ToInt32(section),
                    Convert.ToInt32(course), Convert.ToInt32(status)
                   ).Select(o => MapDtoToModel(o)));
            }));
        }
    }
}