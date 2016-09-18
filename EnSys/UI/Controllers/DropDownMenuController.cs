using BL;
using BL.Dto;
using BL.Services;
using System.Collections.Generic;
using System.Web.Mvc;
using Util.Enums;

namespace UI.Controllers
{
    public class DropDownMenuController : BaseController
    {
        public JsonResult Instructors()
        {
            var records = Transaction.Scope(scope => scope.Service<InstructorService, IEnumerable<IOption>>(service => service.GetRecordsBindToDropDown()));
            return JsonResultSuccess(records);
        }

        public JsonResult Courses()
        {
            var records = Transaction.Scope(scope => scope.Service<CourseService, IEnumerable<IOption>>(service => service.GetRecordsBindToDropDown()));
            return JsonResultSuccess(records);
        }

        public JsonResult Subjects()
        {
            var records = Transaction.Scope(scope => scope.Service<SubjectService, IEnumerable<IOption>>(service => service.GetRecordsBindToDropDown()));
            return JsonResultSuccess(records);
        }

        public JsonResult Sections(int? id)
        {
            var records = Transaction.Scope(scope => scope.Service<SectionService, IEnumerable<IOption>>(service => service.GetRecordsBindToDropDown(id)));
            return JsonResultSuccess(records);
        }

        public JsonResult SectionsBySubjectId(int id)
        {
            var records = Transaction.Scope(scope => scope.Service<SectionService, IEnumerable<IOption>>(service => service.GetRecordsBindToDropDownBySubjectId(id)));
            return JsonResultSuccess(records);
        }

        public JsonResult Rooms()
        {
            var records = Transaction.Scope(scope => scope.Service<RoomService, IEnumerable < IOption >> (service => service.GetRecordsBindToDropDown()));
            return JsonResultSuccess(records);
        }

        public JsonResult Gender()
        {
            var records = Transaction.Scope(scope => scope.Service<OptionService, IEnumerable<IOption>>(service => service.GetRecordsBindToDropDown(OptionType.Gender)));
            return JsonResultSuccess(records);
        }

        public JsonResult YearLevels()
        {
            var records = Transaction.Scope(scope => scope.Service<OptionService, IEnumerable<IOption>>(service => service.GetRecordsBindToDropDown(OptionType.YearLevel)));
            return JsonResultSuccess(records);
        }

        public JsonResult Units()
        {
            var records = Transaction.Scope(scope => scope.Service<OptionService, IEnumerable<IOption>>(service => service.GetRecordsBindToDropDown(OptionType.Unit)));
            return JsonResultSuccess(records);
        }

        public JsonResult Status()
        {
            var records = Transaction.Scope(scope => scope.Service<OptionService, IEnumerable<IOption>>(service => service.GetRecordsBindToDropDown(OptionType.Status)));
            return JsonResultSuccess(records);
        }

        public JsonResult Days()
        {
            var records = Transaction.Scope(scope => scope.Service<OptionService, IEnumerable<IOption>>(service => service.GetRecordsBindToDropDown(OptionType.DayOfWeek)));
            return JsonResultSuccess(records);
        }
    }
}