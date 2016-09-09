using BL.Dto;
using BL.Interfaces;
using BL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UI.Models;

namespace UI.Controllers
{
    public class StudentController : BaseController
    {
        [HttpPost]
        public ActionResult Create(StudentWithPersonInfo model)
        {
            if (ModelState.IsValid)
            {
                Service<StudentService>(service => service.AddStudent(model));
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(StudentModel model)
        {
            if (ModelState.IsValid)
            {
                Service<StudentService>(service => service.UpdateStudent(model));
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult EditPersonInfo(PersonalInfoModel model)
        {
            if (ModelState.IsValid)
            {
                Service<StudentService>(service => service.UpdateStudentPersonalInfo(model));
            }
            return View(model);
        }

        private ContactInfoModel MapDtoToContactInfoModel(IContactInfo dto)
        {
            return new ContactInfoModel();
        }

        [HttpGet]
        public ActionResult EditContactInfo(int id)
        {
            ContactInfoModel model = Service<StudentService, ContactInfoModel>(service => MapDtoToContactInfoModel(service.GetStudentContactInfo(id)));
            return View(model);
        }

        [HttpPost]
        public ActionResult EditContactInfo(ContactInfoModel model)
        {
            if (ModelState.IsValid)
            {
                Service<StudentService>(service => service.UpdateStudentContactInfo(model));
            }
            return View(model);
        }
    }
}