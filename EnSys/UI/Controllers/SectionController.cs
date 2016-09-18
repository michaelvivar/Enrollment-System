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
                Level = dto.Level,
                Students = dto.Students
            };
        }

        [Route("")]
        public ActionResult Index()
        {
            return Transaction.Scope(scope => scope.Service<SectionService, ActionResult>(service => View(service.GetSections().Select(o => MapDtoToModel(o)))));
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
                Transaction.Scope(scope => scope.Service<SectionService>(service => service.AddSection(model)));
                return JsonUrlSuccess(Url.Action("Index"));
            }
            return JsonFormError(ModelState);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            return Transaction.Scope(scope => scope.Service<SectionService, ActionResult>(service => View(MapDtoToModel(service.GetSection(id)))));
        }

        [HttpPost]
        public ActionResult Edit(ValidateSectionModel model)
        {
            if (ModelState.IsValid)
            {
                Transaction.Scope(scope => scope.Service<SectionService>(service => service.UpdateSection(model)));
                return JsonUrlSuccess(Url.Action("Index"));
            }
            return JsonFormError(ModelState);
        }
    }
}