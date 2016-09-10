using BL.Dto;
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
        public void Dispose()
        {
            
        }

        private Room MapDtoToEntity(IRoom dto)
        {
            return new Room();
        }

        private IRoom MapEntityToDto(Room entity)
        {
            return new RoomDto();
        }

        public void AddRoom(IRoom dto)
        {
            Db.UnitOfWork(ouw => ouw.Repository<Room>(repo => repo.Add(MapDtoToEntity(dto))));
        }

        public void UpdateRoom(IRoom dto)
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

        public IRoom GetRoomById(int id)
        {
            return Db.Context(context =>
            {
                return (from a in context.Rooms
                        select new RoomDto
                        {
                            Id = a.Id,
                            Number = a.Number,
                            Capacity = a.Capacity,
                            Remarks = a.Remarks,
                            Status = a.Status
                        }).FirstOrDefault();
            });
        }

        public IEnumerable<IRoom> GetAllRooms()
        {
            return Db.Context(context =>
            {
                return (from a in context.Rooms
                        where a.Status == Status.Active
                        orderby a.Number
                        select new RoomDto
                        {
                            Id = a.Id,
                            Number = a.Number,
                            Capacity = a.Capacity,
                            Remarks = a.Remarks,
                            Status = a.Status
                        }).ToList();
            });
        }
    }
}
