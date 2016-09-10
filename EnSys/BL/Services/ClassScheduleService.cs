using BL.Dto;
using BL.Interfaces;
using DL;
using DL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public class ClassScheduleService : BaseService, IService
    {
        public void Dispose()
        {
            
        }

        private IClassSchedule MapEntityToDto(ClassSchedule entity)
        {
            return new ClassScheduleDto();
        }

        private ClassSchedule MapDtoToEntity(IClassSchedule dto)
        {
            return new ClassSchedule();
        }

        public void AddClassSchedule(IClassSchedule dto)
        {
            Db.UnitOfWork(uow => uow.Repository<ClassSchedule>(repo =>
            {
                IList<ClassSchedule> classes = new List<ClassSchedule>();
                foreach(DayOfWeek day in dto.Days)
                {
                    classes.Add(MapDtoToEntity(dto));
                }
                repo.AddRange(classes);
            }));
        }

        public void UpdateClassSchedule(IClassSchedule dto)
        {
            Db.UnitOfWork(uow => uow.Repository<ClassSchedule>(repo => repo.Update(MapDtoToEntity(dto))));
        }

        public void DeleteClassSchedule(int id)
        {
            Db.UnitOfWork(uow => uow.Repository<ClassSchedule>(repo =>
            {
                ClassSchedule entity = repo.Get(id);
                repo.Remove(entity);
            }));
        }

        public IEnumerable<IClassSchedule> GetClassesByStudentId(int id)
        {
            return Db.Context(context =>
            {
                return (from a in context.StudentClassMapping
                        join b in context.Classes
                        on a.StudentId equals b.Id
                        where a.Id == id
                        orderby new { b.Day, b.TimeStart }
                        select new ClassScheduleDto
                        {
                            Id = b.Id,
                            RoomId = b.RoomId,
                            Room = b.Room.Number,
                            TimeStart = b.TimeStart,
                            TimeEnd = b.TimeEnd,
                            Capacity = b.Capacity,
                            Day = b.Day,
                            InstructorId = b.InstructorId,
                            InstructorFirstName = b.Instructor.Person.FirstName,
                            InstructorLastName = b.Instructor.Person.LastName,
                            Remarks = b.Remarks,
                            SubjectId = b.SubjectId,
                            Subject = b.Subject.Code
                        }).ToList();
            });
        }

        public IEnumerable<IClassSchedule> GetClassesByRoomId(int id)
        {
            return Db.Context(context =>
            {
                return (from a in context.Classes
                        where a.RoomId == id
                        orderby new { a.Day, a.TimeStart }
                        select new ClassScheduleDto
                        {
                            Id = a.Id,
                            RoomId = a.RoomId,
                            Room = a.Room.Number,
                            TimeStart = a.TimeStart,
                            TimeEnd = a.TimeEnd,
                            Capacity = a.Capacity,
                            Day = a.Day,
                            InstructorId = a.InstructorId,
                            InstructorFirstName = a.Instructor.Person.FirstName,
                            InstructorLastName = a.Instructor.Person.LastName,
                            Remarks = a.Remarks,
                            SubjectId = a.SubjectId,
                            Subject = a.Subject.Code
                        }).ToList();
            });
        }
    }
}
