using BL.Dto;
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
                Capacity = (int)dto.Capacity,
                Day = (DayOfWeek)dto.Day,
                TimeStart = (DateTime)dto.TimeStart,
                TimeEnd = (DateTime)dto.TimeEnd,
                Remarks = dto.Remarks,
                InstructorId = (int)dto.InstructorId,
                SubjectId = (int)dto.SubjectId,
                SectionId = (int)dto.SectionId,
                RoomId = (int)dto.RoomId
            };
        }


        public void AddClassSchedule(IClassSchedule dto)
        {
            Repository<ClassSchedule>(repo =>
            {
                IList<ClassSchedule> classes = new List<ClassSchedule>();
                foreach(DayOfWeek day in dto.Days.Where(o => o != 0))
                {
                    dto.Day = day;
                    var entity = MapDtoToEntity(dto);
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
            return Classes().OrderBy(o => o.Day).ThenBy(o => o.Room).ThenBy(o => o.TimeStart).ToList();
        }

        public IEnumerable<IClassSchedule> GetClassesFiltered(int day, int instructor, int subject, int section, int room)
        {
            return Classes().OrderBy(o => o.Day)
                .Where(o => (day == 0 ? true : o.Day == (DayOfWeek)day) &&
                (instructor == 0 ? true : o.InstructorId == instructor) &&
                (subject == 0 ? true : o.SubjectId == subject) &&
                (section == 0 ? true : o.SectionId == section) &&
                (room == 0 ? true : o.RoomId == room))
                .OrderBy(o => o.Day).ThenBy(o => o.Room).ThenBy(o => o.TimeStart).ToList();
        }

        public IEnumerable<IClassSchedule> GetClassesByStudentId(int id)
        {
            return Query(context =>
            {
                return (from a in Classes()
                        join b in context.Students
                        on a.SectionId equals b.SectionId
                        where b.Id == id
                        orderby a.Day, a.TimeStart
                        select a).ToList();
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

    public class ClassScheduleValidatorService : ValidatorService, IService
    {
        internal ClassScheduleValidatorService(Context context) : base(context) { }

    }
}
