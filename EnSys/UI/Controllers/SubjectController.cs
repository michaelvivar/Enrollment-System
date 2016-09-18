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
            return Transaction.Scope(scope => scope.Service<SubjectService, ActionResult>(service => View(service.GetSubjects().Select(o => MapDtoToModel(o)))));
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
                Transaction.Scope(scope => scope.Service<SubjectService>(service => service.AddSubject(model)));
                return JsonUrlSuccess(Url.Action("Index"));
            }
            return JsonFormError(ModelState);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            return Transaction.Scope(scope => scope.Service<SubjectService, ActionResult>(service => View(MapDtoToModel(service.GetSubject(id)))));
        }

        [HttpPost]
        public ActionResult Edit(ValidateSubjectModel model)
        {
            if (ModelState.IsValid)
            {
                Transaction.Scope(scope => scope.Service<SubjectService>(service => service.UpdateSubject(model)));
                return JsonUrlSuccess(Url.Action("Index"));
            }
            return JsonFormError(ModelState);
        }
    }
}