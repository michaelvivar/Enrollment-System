using BL;
using BL.Interfaces;
using BL.Services;
using Nelibur.ObjectMapper;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using UI.Models;

namespace UI.Controllers
{
    public class CourseController : BaseController
    {
        private CourseModel MapDtoToModel(ICourse dto)
        {
            return new CourseModel
            {
                Id = dto.Id,
                Code = dto.Code,
                Remarks = dto.Remarks,
                Status = dto.Status
            };
        }

        [Route("")]
        public ActionResult Index()
        {
            return Transaction.Scope(scope => scope.Service<CourseService, ActionResult>(service => View(service.GetCourses().Select(o => MapDtoToModel(o)))));
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(new CourseModel());
        }

        [HttpPost]
        public ActionResult Create(ValidateCourseModel model)
        {
            if (ModelState.IsValid)
            {
                Transaction.Scope(scope => scope.Service<CourseService>(service => service.AddCourse(model)));
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            return Transaction.Scope(scope => scope.Service<CourseService, ActionResult>(service => View(MapDtoToModel(service.GetCourse(id)))));
        }

        [HttpPost]
        public ActionResult Edit(ValidateCourseModel model)
        {
            if (ModelState.IsValid)
            {
                Transaction.Scope(scope => scope.Service<CourseService>(service => service.UpdateCourse(model)));
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}