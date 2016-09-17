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
    public class InstructorController : BaseController
    {
        private InstructorModel MapDtoToModel(IInstructor dto)
        {
            return TinyMapper.Map<InstructorModel>(dto);
        }

        [Route("")]
        public ActionResult Index()
        {
            return Transaction.Scope(scope => scope.Service<InstructorService, ActionResult>(service => View(service.GetInstructors().Select(o => MapDtoToModel(o)))));
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(new InstructorModel());
        }

        [HttpPost]
        public ActionResult Create(ValidateInstructorModel model)
        {
            if (ModelState.IsValid)
            {
                Transaction.Scope(scope => scope.Service<InstructorService>(service => service.AddInstructor(model)));
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            return Transaction.Scope(scope => scope.Service<InstructorService, ActionResult>(service => View(MapDtoToModel(service.GetInstructor(id)))));
        }

        [HttpPost]
        public ActionResult Edit(ValidateInstructorModel model)
        {
            if (ModelState.IsValid)
            {
                Transaction.Scope(scope => scope.Service<InstructorService>(service => service.UpdateInstructor(model)));
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}