using BL.Dto;
using BL.Interfaces;
using BL.Services;
using Nelibur.ObjectMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UI.Models;
using Util.Enums;

namespace UI.Controllers
{
    public class StudentController : BaseController
    {
        private StudentModel MapDtoToModel(IStudent dto)
        {
            return TinyMapper.Map<StudentModel>(dto);
        }

        public ActionResult Index()
        {
            IEnumerable<StudentModel> students = Service<StudentService, IEnumerable<StudentModel>>(service => service.GetAllActiveStudents().Select(o => MapDtoToModel(o)));
            return View(students);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(new StudentModel());
        }

        [HttpPost]
        public ActionResult Create(ValidateStudentModel model)
        {
            if (ModelState.IsValid)
            {
                Service<StudentService>(service => service.AddStudent(model));
            }
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            StudentModel model = Service<StudentService, StudentModel>(service => MapDtoToModel(service.GetStudentById(id)));
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(ValidateStudentModel model)
        {
            if (ModelState.IsValid)
            {
                Service<StudentService>(service => service.UpdateStudent(model));
            }
            return View(model);
        }
    }
}