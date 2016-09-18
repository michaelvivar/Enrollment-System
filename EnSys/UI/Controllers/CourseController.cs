using BL;
using BL.Dto;
using BL.Services;
using System.Linq;
using System.Web.Mvc;
using UI.Models;
using Util.Enums;

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
                Status = (Status)dto.Status,
                Students = (int)dto.Students
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
                return JsonUrlSuccess(Url.Action("Index"));
            }
            return JsonFormError(ModelState);
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
                return JsonUrlSuccess(Url.Action("Index"));
            }
            return JsonFormError(ModelState);
        }
    }
}