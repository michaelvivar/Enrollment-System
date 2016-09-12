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
        public ClassScheduleService(Context context) : base(context) { }

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
            Repository<ClassSchedule>(repo =>
            {
                IList<ClassSchedule> classes = new List<ClassSchedule>();
                foreach(DayOfWeek day in dto.Days)
                {
                    classes.Add(MapDtoToEntity(dto));
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
