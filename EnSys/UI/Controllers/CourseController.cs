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
            return TinyMapper.Map<CourseModel>(dto);
        }

        public ActionResult Index()
        {
            IEnumerable<CourseModel> students = Service<CourseService, IEnumerable<CourseModel>>(service => service.GetAllActiveCourses().Select(o => MapDtoToModel(o)));
            return View(students);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(new CourseModel());
        }

        [HttpPost]
        public ActionResult Create(CourseModel model)
        {
            if (ModelState.IsValid)
            {
                Service<CourseService>(service => service.AddCourse(model));
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(CourseModel model)
        {
            if (ModelState.IsValid)
            {
                Service<CourseService>(service => service.UpdateCourse(model));
            }
            return View(model);
        }
    }
}