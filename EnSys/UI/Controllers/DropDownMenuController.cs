using BL;
using BL.Dto;
using BL.Interfaces;
using BL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Util.Enums;

namespace UI.Controllers
{
    public class DropDownMenuController : BaseController
    {
        public JsonResult Instructors()
        {
            var records = Transaction.Scope(scope => scope.Service<InstructorService, IEnumerable<IOption>>(service => service.GetRecordsBindToDropDown()));
            return Json(records, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Courses()
        {
            var records = Transaction.Scope(scope => scope.Service<CourseService, IEnumerable<IOption>>(service => service.GetRecordsBindToDropDown()));
            return Json(records, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Subjects()
        {
            var records = Transaction.Scope(scope => scope.Service<SubjectService, IEnumerable<IOption>>(service => service.GetRecordsBindToDropDown()));
            return Json(records, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Sections(int? id)
        {
            var records = Transaction.Scope(scope => scope.Service<SectionService, IEnumerable<IOption>>(service => service.GetRecordsBindToDropDown(id)));
            return Json(records, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Rooms()
        {
            var records = Transaction.Scope(scope => scope.Service<RoomService, IEnumerable < IOption >> (service => service.GetRecordsBindToDropDown()));
            return Json(records, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Gender()
        {
            var records = Transaction.Scope(scope => scope.Service<OptionService, IEnumerable<IOption>>(service => service.GetRecordsBindToDropDown(OptionType.Gender)));
            return Json(records, JsonRequestBehavior.AllowGet);
        }

        public JsonResult YearLevels()
        {
            var records = Transaction.Scope(scope => scope.Service<OptionService, IEnumerable<IOption>>(service => service.GetRecordsBindToDropDown(OptionType.YearLevel)));
            return Json(records, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Units()
        {
            var records = Transaction.Scope(scope => scope.Service<OptionService, IEnumerable<IOption>>(service => service.GetRecordsBindToDropDown(OptionType.Unit)));
            return Json(records, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Status()
        {
            var records = Transaction.Scope(scope => scope.Service<OptionService, IEnumerable<IOption>>(service => service.GetRecordsBindToDropDown(OptionType.Status)));
            return Json(records, JsonRequestBehavior.AllowGet);
        }
    }
}