﻿using BL.Dto;
using BL.Interfaces;
using DL;
using DL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using Util.Enums;

namespace BL.Services
{
    public class CourseService : BaseService, IService
    {
        public CourseService(Context context) : base(context) { }

        public void Dispose()
        {

        }

        private Course MapDtoToEntity(ICourse dto)
        {
            return new Course
            {
                Id = dto.Id,
                Code = dto.Code,
                Remarks = dto.Remarks,
                Status = dto.Status
            };
        }

        public void AddCourse(ICourse dto)
        {
            UnitOfWork(uow => uow.Repository<Course>(repo =>
            {
                Course course = MapDtoToEntity(dto);
                repo.Add(course);
            }));
        }

        public void UpdateCourse(ICourse dto)
        {
            UnitOfWork(uow => uow.Repository<Course>(repo =>
            {
                Course course = MapDtoToEntity(dto);
                repo.Update(course, "Status");
            }));
        }

        private void InsertOrDeleteMapping(int courseId, IEnumerable<ISubject> list)
        {
            if (list != null && list.Count() > 0)
            {
                UnitOfWork(uow => uow.Repository<CourseSubjectMapping>(repo =>
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
                UnitOfWork(uow => uow.Repository<CourseSubjectMapping>(repo =>
                {
                    var mapping = repo.Get(o => o.CourseId == courseId);
                    if (mapping != null && mapping.Count() > 0)
                        repo.RemoveRange(mapping);
                }));
            }
        }

        public void DeleteCourse(int id)
        {
            UnitOfWork(uow => uow.Repository<Course>(repo => repo.Remove(repo.SingleOrDefault(o => o.Id == id))));
        }

        public void ActivateCourse(int id)
        {
            UnitOfWork(uow =>
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
            UnitOfWork(uow =>
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
            return Query(context =>
            {
                var course = (from a in context.Courses
                              where a.Status == Status.Active && a.Id == id
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
            return Courses().Where(o => o.Status == Status.Active).OrderBy(o => o.Code).ToList();
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
