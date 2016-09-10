using BL.Dto;
using BL.Interfaces;
using DL;
using DL.Entities;
using System.Collections.Generic;
using System.Linq;
using Util.Enums;

namespace BL.Services
{
    public class CourseService : BaseService, IService
    {
        public void Dispose()
        {

        }

        private Course MapDtoToEntity(ICourse dto)
        {
            return new Course();
        }

        private ICourse MapEntityToDto(Course entity)
        {
            return new CourseDto();
        }

        public void AddCourse(ICourse dto)
        {
            Db.UnitOfWork(uow => uow.Repository<Course>(repo =>
            {
                Course course = MapDtoToEntity(dto);
                repo.Add(course);
                if (dto.Subjects != null && dto.Subjects.Count() > 0)
                {
                    List<CourseSubjectMapping> mapping = dto.Subjects.Select(subject => new CourseSubjectMapping { Course = course, SubjectId = subject.Id }).ToList();
                    uow.Repository<CourseSubjectMapping>(r => r.AddRange(mapping));
                }
            }));
        }

        public void UpdateCourse(ICourse dto)
        {
            Db.UnitOfWork(uow => uow.Repository<Course>(repo =>
            {
                Course course = MapDtoToEntity(dto);
                repo.Update(course);
                InsertOrDeleteMapping(course.Id, dto.Subjects);
            }));
        }

        private void InsertOrDeleteMapping(int courseId, IEnumerable<ISubject> list)
        {
            if (list != null && list.Count() > 0)
            {
                Db.UnitOfWork(uow => uow.Repository<CourseSubjectMapping>(repo =>
                {
                    var mapping = repo.Get(o => o.CourseId == courseId).ToList();
                    var delete = mapping.Where(o => !list.Select(s => s.Id).Contains(o.SubjectId));
                    if (delete != null && delete.Count() > 0)
                        repo.RemoveRange(delete);

                    var insert = list.Where(o => !mapping.Select(s => s.SubjectId).Contains(o.Id)).Select(o => new CourseSubjectMapping
                    {
                        CourseId = courseId,
                        SubjectId = o.Id
                    });
                    if (insert != null && insert.Count() > 0)
                        repo.AddRange(insert);
                }));
            }
            else
            {
                Db.UnitOfWork(uow => uow.Repository<CourseSubjectMapping>(repo =>
                {
                    var mapping = repo.Get(o => o.CourseId == courseId);
                    if (mapping != null && mapping.Count() > 0)
                        repo.RemoveRange(mapping);
                }));
            }
        }

        public void DeleteCourse(int id)
        {
            Db.UnitOfWork(uow => uow.Repository<Course>(repo => repo.Remove(repo.SingleOrDefault(o => o.Id == id))));
        }

        public void ActivateCourse(int id)
        {
            Db.UnitOfWork(uow =>
            {
                uow.Repository<Course>(repo =>
                {
                    Course course = repo.Get(id);
                    course.Status = Status.Active;
                    repo.Update(course);
                });
            });
        }

        public void InactivateCourse(int id)
        {
            Db.UnitOfWork(uow =>
            {
                uow.Repository<Course>(repo =>
                {
                    Course course = repo.Get(id);
                    course.Status = Status.Inactive;
                    repo.Update(course);
                });
            });
        }

        public ICourse GetCourseById(int id)
        {
            return Db.Context(context =>
            {
                var course = (from a in context.Courses
                              where a.Status == Status.Active
                              orderby a.Code
                              select new CourseDto
                              {
                                  Id = a.Id,
                                  Code = a.Code,
                                  Remarks = a.Remarks,
                                  Status = a.Status
                              }).FirstOrDefault();
                course.Subjects = (from a in context.CourseSubjectMapping
                                   join b in context.Subjects
                                   on a.SubjectId equals b.Id
                                   where a.CourseId == id && b.Status == Status.Active && a.Course.Status == Status.Active
                                   select new SubjectDto
                                   {
                                       Id = b.Id,
                                       Code = b.Code,
                                       Level = b.Level,
                                       Remarks = b.Remarks,
                                       Units = b.Units,
                                       Status = b.Status
                                   }).ToList();
                return course;
            });
        }

        public IEnumerable<ICourse> GetAllActiveCourses()
        {
            return Db.Context(context =>
            {
                return (from a in context.Courses
                        where a.Status == Status.Active
                        orderby a.Code
                        select new CourseDto
                        {
                            Id = a.Id,
                            Code = a.Code,
                            Remarks = a.Remarks,
                            Status = a.Status
                        }).ToList();
            });
        }
    }
}
