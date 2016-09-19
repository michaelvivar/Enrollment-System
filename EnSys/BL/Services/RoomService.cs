using BL.Dto;
using DL;
using DL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using Util.Enums;

namespace BL.Services
{
    public class RoomService : BaseService, IService
    {
        internal RoomService(Context context) : base(context) { }

        internal Room MapDtoToEntity(IRoom dto)
        {
            return new Room
            {
                Id = dto.Id,
                Number = dto.Number,
                Capacity = (int)dto.Capacity,
                Remarks = dto.Remarks,
                Status = (Status)dto.Status
            };
        }


        public void AddRoom(IRoom dto)
        {
            Repository<Room>(repo => repo.Add(MapDtoToEntity(dto)).Save());
        }

        public void UpdateRoom(IRoom dto)
        {
            Repository<Room>(repo => repo.Update(MapDtoToEntity(dto)).Save());
        }

        public void DeleteRoom(int id)
        {
            Repository<Room>(repo =>
            {
                Room entity = repo.Get(id);
            });
        }


        internal IQueryable<IRoom> Rooms()
        {
            return Query(context =>
            {
                return (from a in context.Rooms
                        select new RoomDto
                        {
                            Id = a.Id,
                            Number = a.Number,
                            Capacity = a.Capacity,
                            Remarks = a.Remarks,
                            Status = a.Status
                        });
            });
        }

        public IRoom GetRoom(int id)
        {
            return Rooms().Where(o => o.Id == id).FirstOrDefault();
        }

        public IEnumerable<IRoom> GetRooms()
        {
            return Rooms().OrderBy(o => o.Number).ToList();
        }

        public IEnumerable<IOption> GetRecordsBindToDropDown()
        {
            return Rooms().Where(o => o.Status == Status.Active).OrderBy(o => o.Number)
                    .Select(o => new OptionDto { Text = o.Number, Value = o.Id }).ToList();
        }
    }

    public class RoomValidatorService : BaseService, IService
    {
        internal RoomValidatorService(Context context) : base(context) { }

        public bool CheckRoomNumberExists(int id, string room)
        {
            var record = Query(context => context.Rooms.Where(o => o.Number == room).Select(o => o.Id)).FirstOrDefault();
            if (record != 0)
            {
                if (id == 0)
                    return true;

                if (record == id)
                    return false;

                return true;
            }
            return false;
        }

        public bool CheckRoomAvailavility(int classId, int? roomId, DateTime start, DateTime end, DayOfWeek day)
        {
            bool available = true;
            int timeStart = start.Hour * 100 + start.Minute;
            int timeEnd = end.Hour * 100 + end.Minute;
            var records = Query(context =>
            {
                return (from a in context.Classes where a.RoomId == roomId && a.Day == day && a.Id != classId select new { a.TimeStart, a.TimeEnd }).ToList();
            });
            
            records.ForEach(o =>
            {
                int a = o.TimeStart.Hour * 100 + o.TimeStart.Minute;
                int b = o.TimeEnd.Hour * 100 + o.TimeEnd.Minute;
                if (timeStart > a && timeStart < b)
                    available = false;

                if (timeEnd > a && timeEnd < b)
                    available = false;
            });
            return available;
        }
    }
}
