using BL;
using BL.Dto;
using BL.Services;
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
            return Transaction.Scope(scope => scope.Service<RoomService, ActionResult>(service => View(service.GetRooms().Select(o => MapDtoToModel(o)))));
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
                Transaction.Scope(scope => scope.Service<RoomService>(service => service.AddRoom(model)));
                return JsonUrlSuccess(Url.Action("Index"));
            }
            return JsonFormError(ModelState);
        }
         
        [HttpGet]
        public ActionResult Edit(int id)
        {
            return Transaction.Scope(scope => scope.Service<RoomService, ActionResult>(service => View(MapDtoToModel(service.GetRoom(id)))));
        }

        [HttpPost]
        public ActionResult Edit(ValidateRoomModel model)
        {
            if (ModelState.IsValid)
            {
                Transaction.Scope(scope => scope.Service<RoomService>(service => service.UpdateRoom(model)));
                return JsonUrlSuccess(Url.Action("Index"));
            }
            return JsonFormError(ModelState);
        }

        public JsonResult Delete(int id)
        {
            if (Transaction.Scope(scope => scope.Service<RoomService, bool>(service => service.DeleteRoom(id))))
                return JsonUrlSuccess(Url.Action("Index"));

            return JsonResultError("Failed to delete a record");
        }

        public JsonResult GetCapacityByRoomId(int id)
        {
            var capacity = Transaction.Scope(scope => scope.Service<RoomService, int?>(service => service.GetRoom(id).Capacity));
            return JsonResultSuccess(new { Capacity = capacity }, string.Empty);
        }
    }
}