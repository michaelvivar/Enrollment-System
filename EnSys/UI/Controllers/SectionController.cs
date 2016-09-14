using BL.Interfaces;
using BL.Services;
using Nelibur.ObjectMapper;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using UI.Models;

namespace UI.Controllers
{
    public class SectionController : BaseController
    {
        private SectionModel MapDtoToModel(ISection dto)
        {
            return new SectionModel
            {
                Id = dto.Id,
                Code = dto.Code,
                Remarks = dto.Remarks,
                Status = dto.Status,
                Level = dto.Level
            };
        }

        [Route("")]
        public ActionResult Index()
        {
            IEnumerable<SectionModel> students = Service<SectionService, IEnumerable<SectionModel>>(service => service.GetAllSections().Select(o => MapDtoToModel(o)));
            return View(students);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(new SectionModel());
        }

        [HttpPost]
        public ActionResult Create(ValidateSectionModel model)
        {
            if (ModelState.IsValid)
            {
                Service<SectionService>(service => service.AddSection(model));
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            SectionModel model = Service<SectionService, SectionModel>(service => MapDtoToModel(service.GetSectionById(id)));
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(ValidateSectionModel model)
        {
            if (ModelState.IsValid)
            {
                Service<SectionService>(service => service.UpdateSection(model));
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}