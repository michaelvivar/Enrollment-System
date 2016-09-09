using BL.Dto;
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
    public class CourseService : BaseService, IService
    {
        public void Dispose()
        {

        }

        private Course MapDtoToEntity(CourseDto dto)
        {
            return new Course();
        }

        private CourseDto MapEntityToDto(Course entity)
        {
            return new CourseDto();
        }

        public List<CourseDto> GetAllActiveCourses()
        {
            return GetAllActive().Select(o => MapEntityToDto(o)).ToList();
        }

        private IEnumerable<Course> GetAllActive()
        {
            return Db.UnitOfWork(uow => uow.Repository<Course, IEnumerable<Course>>(repo => repo.Get(o => o.Status == Status.Active)));
        }

        public void AddCourse(CourseDto dto)
        {
            Db.UnitOfWork(uow => uow.Repository<Course>(repo =>
            {
                Course course = MapDtoToEntity(dto);
                repo.Add(course);
                if (dto.Subjects != null && dto.Subjects.Count > 0)
                {
                    List<CourseSubjectMapping> mapping = dto.Subjects.Select(subject => new CourseSubjectMapping { Course = course, SubjectId = subject.Id }).ToList();
                    uow.Repository<CourseSubjectMapping>(r => r.AddRange(mapping));
                }
            }));
        }

        public void UpdateCourse(CourseDto dto)
        {
            Db.UnitOfWork(uow => uow.Repository<Course>(repo =>
            {
                Course course = MapDtoToEntity(dto);
                repo.Update(course);
                InsertOrDeleteMapping(course.Id, dto.Subjects);
            }));
        }

        private void InsertOrDeleteMapping(int courseId, List<SubjectDto> list)
        {
            if (list != null && list.Count > 0)
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

        public void DeactivateCourse(int id)
        {
            Db.UnitOfWork(uow =>
            {
                uow.Repository<Course>(repo =>
                {
                    Course course = repo.Get(id);
                    course.Status = Status.Deactive;
                    repo.Update(course);
                });
            });
        }
    }
}
