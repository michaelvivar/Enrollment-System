﻿using BL.Dto;
using BL.Interfaces;
using DL;
using DL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
                Status = dto.Status
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
                        select new CourseDto
                        {
                            Id = a.Id,
                            Code = a.Code,
                            Remarks = a.Remarks,
                            Status = a.Status
                        });
            });
        }

        public ICourse GetCourseById(int id)
        {
            return Courses().SingleOrDefault(o => o.Id == id);

            //return Query(context =>
            //{
            //    var course = (from a in context.Courses
            //                  where a.Status == Status.Active && a.Id == id
            //                  orderby a.Code
            //                  select new CourseDto
            //                  {
            //                      Id = a.Id,
            //                      Code = a.Code,
            //                      Remarks = a.Remarks,
            //                      Status = a.Status
            //                  }).FirstOrDefault();

            //    course.Subjects = (from a in context.CourseSubjectMapping
            //                       join b in context.Subjects
            //                       on a.SubjectId equals b.Id
            //                       where a.CourseId == id && b.Status == Status.Active && a.Course.Status == Status.Active
            //                       select new SubjectDto
            //                       {
            //                           Id = b.Id,
            //                           Code = b.Code,
            //                           Level = b.Level,
            //                           Remarks = b.Remarks,
            //                           Units = b.Units,
            //                           Status = b.Status
            //                       }).ToList();
            //    return course;
            //});
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
