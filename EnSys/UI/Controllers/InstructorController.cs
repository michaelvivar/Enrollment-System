using BL;
using BL.Dto;
using BL.Services;
using Nelibur.ObjectMapper;
using System.Linq;
using System.Web.Mvc;
using UI.Models;

namespace UI.Controllers
{
    public class InstructorController : BaseController
    {
        private InstructorModel MapDtoToModel(IInstructor dto)
        {
            return TinyMapper.Map<InstructorModel>(dto);
        }

        private ClassScheduleModel MapScheduleDtoToModel(IClassSchedule dto)
        {
            return TinyMapper.Map<ClassScheduleModel>(dto);
        }

        [Route("")]
        public ActionResult Index()
        {
            return Transaction.Scope(scope => scope.Service<InstructorService, ActionResult>(service => View(service.GetInstructors().Select(o => MapDtoToModel(o)))));
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(new InstructorModel());
        }

        [HttpPost]
        public ActionResult Create(ValidateInstructorModel model)
        {
            if (ModelState.IsValid)
            {
                Transaction.Scope(scope => scope.Service<InstructorService>(service => service.AddInstructor(model)));
                return JsonUrlSuccess(Url.Action("Index"));
            }
            return JsonFormError(ModelState);
        }

        public ActionResult Edit(int id)
        {
            return Transaction.Scope(scope => scope.Service<InstructorService, ActionResult>(service => View(MapDtoToModel(service.GetInstructor(id)))));
        }

        [HttpPost]
        public ActionResult Edit(ValidateInstructorModel model)
        {
            if (ModelState.IsValid)
            {
                Transaction.Scope(scope => scope.Service<InstructorService>(service => service.UpdateInstructor(model)));
                return JsonUrlSuccess(Url.Action("Index"));
            }
            return JsonFormError(ModelState);
        }

        public ActionResult Details(int id)
        {
            return Transaction.Scope(scope => scope.Service<InstructorService, ActionResult>(service => View(MapDtoToModel(service.GetInstructor(id)))));
        }

        public ActionResult Schedule(int id)
        {
            return Transaction.Scope(scope => scope.Service<ClassScheduleService, ActionResult>(service => PartialView("Schedule", service.GetClassesByInstructorId(id).Select(o => MapScheduleDtoToModel(o)))));
        }
    }
}