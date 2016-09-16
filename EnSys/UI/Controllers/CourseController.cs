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
            //return TinyMapper.Map<CourseModel>(dto);
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
            IEnumerable<CourseModel> students = Service<CourseService, IEnumerable<CourseModel>>(service => service.GetCourses().Select(o => MapDtoToModel(o)));
            return View(students);
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
                Service<CourseService>(service => service.AddCourse(model));
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            CourseModel model = Service<CourseService, CourseModel>(service => MapDtoToModel(service.GetCourseById(id)));
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(ValidateCourseModel model)
        {
            if (ModelState.IsValid)
            {
                Service<CourseService>(service => service.UpdateCourse(model));
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}