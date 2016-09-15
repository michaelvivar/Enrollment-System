﻿using BL.Dto;
using BL.Interfaces;
using DL;
using DL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util.Enums;

namespace BL.Services
{
    public class RoomService : BaseService, IService
    {
        internal RoomService(Context context) : base(context) { }

        private Room MapDtoToEntity(IRoom dto)
        {
            return new Room
            {
                Id = dto.Id,
                Number = dto.Number,
                Capacity = dto.Capacity,
                Remarks = dto.Remarks,
                Status = dto.Status
            };
        }

        public void AddRoom(IRoom dto)
        {
            Repository<Room>(repo => repo.Add(MapDtoToEntity(dto)));
        }

        public void UpdateRoom(IRoom dto)
        {
            Repository<Room>(repo => repo.Update(MapDtoToEntity(dto)));
        }

        public void DeleteRoom(int id)
        {
            Repository<Room>(repo =>
            {
                Room entity = repo.Get(id);
            });
        }

        public IRoom GetRoomById(int id)
        {
            return Rooms().Where(o => o.Id == id).FirstOrDefault();
        }

        public IEnumerable<IRoom> GetAllRooms()
        {
            return Rooms().Where(o => o.Status == Status.Active).OrderBy(o => o.Number).ToList();
        }

        public IEnumerable<IOption> GetRecordsBindToDropDown()
        {
            return Rooms().Where(o => o.Status == Status.Active).OrderBy(o => o.Number)
                    .Select(o => new OptionDto { Text = o.Number, Value = o.Id }).ToList();
        }
    }
}
