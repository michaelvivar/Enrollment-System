﻿using BL.Dto;
using DL;
using DL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public class RoomService : BaseService, IService
    {
        public void Dispose()
        {
            
        }

        private Room MapDtoToEntity(RoomDto dto)
        {
            return new Room();
        }

        private RoomDto MapEntityToDto(Room entity)
        {
            return new RoomDto();
        }

        public RoomDto Get(int id)
        {
            return Db.UnitOfWork(uow => uow.Repository<Room, RoomDto>(repo => MapEntityToDto(repo.Get(id))));
        }

        public List<RoomDto> GetAll()
        {
            return Db.UnitOfWork(uow => uow.Repository<Room, List<RoomDto>>(repo => repo.All().OrderBy(o => o.Number).Select(o => MapEntityToDto(o)).ToList()));
        }

        public void AddRoom(RoomDto dto)
        {
            Db.UnitOfWork(ouw => ouw.Repository<Room>(repo => repo.Add(MapDtoToEntity(dto))));
        }

        public void UpdateRoom(RoomDto dto)
        {
            Db.UnitOfWork(ouw => ouw.Repository<Room>(repo => repo.Update(MapDtoToEntity(dto))));
        }

        public void DeleteRoom(int id)
        {
            Db.UnitOfWork(ouw => ouw.Repository<Room>(repo =>
            {
                Room entity = repo.Get(id);
            }));
        }
    }
}
