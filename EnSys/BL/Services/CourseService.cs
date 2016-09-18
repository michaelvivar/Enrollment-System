using BL.Dto;
using DL;
using DL.Entities;
using System.Collections.Generic;
using System.Linq;
using Util.Enums;

namespace BL.Services
{
    public class CourseService : BaseService, IService
    {
        internal CourseService(Context context) : base(context) { }

        internal Course MapDtoToEntity(ICourse dto)
        {
            return new Course
            {
                Id = dto.Id,
                Code = dto.Code,
                Remarks = dto.Remarks,
                Status = (Status)dto.Status
            };
        }


        public void AddCourse(ICourse dto)
        {
            Repository<Course>(repo => repo.Add(MapDtoToEntity(dto)).Save());
        }

        public void UpdateCourse(ICourse dto)
        {
            Repository<Course>(repo => repo.Update(MapDtoToEntity(dto)).Save());
        }

        public void DeleteCourse(int id)
        {
            Repository<Course>(repo => repo.Remove(repo.SingleOrDefault(o => o.Id == id)).Save());
        }


        internal IQueryable<ICourse> Courses()
        {
            return Query(context =>
            {
                return (from a in context.Courses
                        let count = context.Students.Where(o => o.CourseId == a.Id).Count()
                        select new CourseDto
                        {
                            Id = a.Id,
                            Code = a.Code,
                            Remarks = a.Remarks,
                            Status = a.Status,
                            Students = count
                        });
            });
        }

        public ICourse GetCourse(int id)
        {
            return Courses().SingleOrDefault(o => o.Id == id);
        }

        public IEnumerable<ICourse> GetCourses()
        {
            return Courses().OrderBy(o => o.Status).ThenBy(o => o.Code).ToList();
        }

        public IEnumerable<IOption> GetRecordsBindToDropDown()
        {
            return Query(context =>
            {
                return Courses().Where(o => o.Status == Status.Active).OrderBy(o => o.Code)
                    .Select(o => new OptionDto { Text = o.Code, Value = o.Id }).ToList();
            });
        }
    }
}
