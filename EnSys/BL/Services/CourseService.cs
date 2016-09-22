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

        public bool DeleteCourse(int id)
        {
            if (Repository<Student, bool>(repo => repo.Get(o => o.CourseId == id).Any()))
                return false;

            Repository<Course>(repo => repo.Remove(repo.Get(id)).Save());
            return true;
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

    public class CourseValidatorService : ValidatorService, IService
    {
        internal CourseValidatorService(Context context) : base(context) { }

        public bool CheckCourseCodeExists(int id, string code)
        {
            return Query(context => context.Courses.Where(o => o.Code == code && ((id == 0) ? true : o.Id != id)).Any());
        }
    }
}
