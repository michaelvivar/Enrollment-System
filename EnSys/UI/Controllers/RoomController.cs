using BL.Interfaces;
using BL.Services;
using Nelibur.ObjectMapper;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using UI.Models;

namespace UI.Controllers
{
    public class RoomController : BaseController
    {
        private RoomModel MapDtoToModel(IRoom dto)
        {
            return new RoomModel
            {
                Id = dto.Id,
                Number = dto.Number,
                Capacity = dto.Capacity,
                Remarks = dto.Remarks,
                Status = dto.Status
            };
        }

        [Route("")]
        public ActionResult Index()
        {
            IEnumerable<RoomModel> students = Service<RoomService, IEnumerable<RoomModel>>(service => service.GetRooms().Select(o => MapDtoToModel(o)));
            return View(students);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(new RoomModel());
        }

        [HttpPost]
        public ActionResult Create(ValidateRoomModel model)
        {
            if (ModelState.IsValid)
            {
                Service<RoomService>(service => service.AddRoom(model));
                return RedirectToAction("Index");
            }
            return View(model);
        }
         
        [HttpGet]
        public ActionResult Edit(int id)
        {
            RoomModel model = Service<RoomService, RoomModel>(service => MapDtoToModel(service.GetRoomById(id)));
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(ValidateRoomModel model)
        {
            if (ModelState.IsValid)
            {
                Service<RoomService>(service => service.UpdateRoom(model));
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}