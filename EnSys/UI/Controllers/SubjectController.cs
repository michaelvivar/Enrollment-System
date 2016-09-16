using BL.Interfaces;
using BL.Services;
using Nelibur.ObjectMapper;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using UI.Models;

namespace UI.Controllers
{
    public class SubjectController : BaseController
    {
        private SubjectModel MapDtoToModel(ISubject dto)
        {
            return new SubjectModel
            {
                Id = dto.Id,
                Code = dto.Code,
                Remarks = dto.Remarks,
                Status = dto.Status,
                Level = dto.Level,
                Units = dto.Units
            };
        }

        [Route("")]
        public ActionResult Index()
        {
            IEnumerable<SubjectModel> students = Service<SubjectService, IEnumerable<SubjectModel>>(service => service.GetSubjects().Select(o => MapDtoToModel(o)));
            return View(students);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(new SubjectModel());
        }

        [HttpPost]
        public ActionResult Create(ValidateSubjectModel model)
        {
            if (ModelState.IsValid)
            {
                Service<SubjectService>(service => service.AddSubject(model));
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            SubjectModel model = Service<SubjectService, SubjectModel>(service => MapDtoToModel(service.GetSubjectById(id)));
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(ValidateSubjectModel model)
        {
            if (ModelState.IsValid)
            {
                Service<SubjectService>(service => service.UpdateSubject(model));
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}