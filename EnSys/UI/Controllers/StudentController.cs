﻿using BL;
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

        [Route("")]
        public ActionResult Index()
        {
            return Transaction.Scope(scope => scope.Service<StudentService, ActionResult>(service => View(service.GetStudents().Select(o => MapDtoToModel(o)))));
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
                Transaction.Scope(scope => scope.Service<StudentService>(service => service.AddStudent(model)));
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            return Transaction.Scope(scope => scope.Service<StudentService, ActionResult>(service => View(MapDtoToModel(service.GetStudent(id)))));
        }

        [HttpPost]
        public ActionResult Edit(ValidateStudentModel model)
        {
            if (ModelState.IsValid)
            {
                Transaction.Scope(scope => scope.Service<StudentService>(service => service.UpdateStudent(model)));
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}