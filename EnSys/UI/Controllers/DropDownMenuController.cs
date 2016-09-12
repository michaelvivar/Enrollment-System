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
            var records = Service<InstructorService, object>(service => service.GetRecordsBindToDropDown());
            return Json(records, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Courses()
        {
            var records = Service<CourseService, IEnumerable<IOption>>(service => service.GetRecordsBindToDropDown());
            return Json(records, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Subjects()
        {
            var records = Service<SubjectService, object>(service => service.GetRecordsBindToDropDown());
            return Json(records, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Rooms()
        {
            var records = Service<RoomService, object>(service => service.GetRecordsBindToDropDown());
            return Json(records, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Gender()
        {
            var records = Service<OptionService, object>(service => service.GetRecordsBindToDropDown(OptionType.Gender));
            return Json(records, JsonRequestBehavior.AllowGet);
        }

        public JsonResult YearLevels()
        {
            var records = Service<OptionService, object>(service => service.GetRecordsBindToDropDown(OptionType.YearLevel));
            return Json(records, JsonRequestBehavior.AllowGet);
        }
    }
}