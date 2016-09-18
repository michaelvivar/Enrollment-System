using BL.Dto;
using BL.Interfaces;
using DL;
using DL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BL.Services
{
    public class ClassScheduleService : BaseService, IService
    {
        internal ClassScheduleService(Context context) : base(context) { }

        internal ClassSchedule MapDtoToEntity(IClassSchedule dto)
        {
            return new ClassSchedule
            {
                Id = dto.Id,
                Capacity = dto.Capacity,
                Day = dto.Day,
                TimeStart = dto.TimeStart,
                TimeEnd = dto.TimeEnd,
                Remarks = dto.Remarks,
                InstructorId = dto.InstructorId,
                SubjectId = dto.SubjectId,
                SectionId = dto.SectionId,
                RoomId = dto.RoomId
            };
        }


        public void AddClassSchedule(IClassSchedule dto)
        {
            Repository<ClassSchedule>(repo =>
            {
                IList<ClassSchedule> classes = new List<ClassSchedule>();
                foreach(DayOfWeek day in dto.Days.Where(o => o != 0))
                {
                    var entity = MapDtoToEntity(dto);
                    entity.Day = day;
                    classes.Add(entity);
                }
                repo.AddRange(classes);
            });
        }

        public void UpdateClassSchedule(IClassSchedule dto)
        {
            Repository<ClassSchedule>(repo => repo.Update(MapDtoToEntity(dto)));
        }

        public void DeleteClassSchedule(int id)
        {
            Repository<ClassSchedule>(repo =>
            {
                ClassSchedule entity = repo.Get(id);
                repo.Remove(entity);
            });
        }


        internal IQueryable<IClassSchedule> Classes()
        {
            return Query(context =>
            {
                return (from a in context.Classes
                        select new ClassScheduleDto
                        {
                            Id = a.Id,
                            RoomId = a.RoomId,
                            Room = a.Room.Number,
                            TimeStart = a.TimeStart,
                            TimeEnd = a.TimeEnd,
                            Capacity = (a.Capacity <= 0) ? a.Room.Capacity : a.Capacity ,
                            Day = a.Day,
                            InstructorId = a.InstructorId,
                            InstructorFirstName = a.Instructor.Person.FirstName,
                            InstructorLastName = a.Instructor.Person.LastName,
                            Remarks = a.Remarks,
                            SubjectId = a.SubjectId,
                            Subject = a.Subject.Code,
                            SectionId = a.SectionId,
                            Section = a.Section.Code
                        });
            });
        }

        public IClassSchedule GetClass(int id)
        {
            return Query(context =>
            {
                return Classes().SingleOrDefault(o => o.Id == id);
            });
        }

        public IEnumerable<IClassSchedule> GetClasses()
        {
            return Classes().OrderBy(o => o.Day).ThenBy(o => o.TimeStart).ToList();
        }

        public IEnumerable<IClassSchedule> GetClassesByStudentId(int id)
        {
            return Query(context =>
            {
                return (from a in context.StudentClassMapping
                        join b in Classes()
                        on a.StudentId equals b.Id
                        where a.Id == id
                        orderby b.Day, b.TimeStart
                        select b);
            });
        }

        public IEnumerable<IClassSchedule> GetClassesByInstructorId(int id)
        {
            return Classes().Where(o => o.InstructorId == id).OrderBy(o => o.Day).ThenBy(o => o.TimeStart).ToList();
        }

        public IEnumerable<IClassSchedule> GetClassesByRoomId(int id)
        {
            return Classes().Where(o => o.RoomId == id).OrderBy(o => o.Day).ThenBy(o => o.TimeStart).ToList();
        }
    }
}
