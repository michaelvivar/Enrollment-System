using BL.Dto;
using BL.Interfaces;
using DL;
using DL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

using System.Data.Entity;
using Util.Enums;
using BL;

namespace BL.Services
{
    public class StudentService : BaseService, IService
    {
        internal StudentService(Context context) : base(context) { }

        internal Student MapDtoToEntity(IStudent dto)
        {
            return new Student
            {
                Id = dto.Id,
                CourseId = dto.CourseId,
                Level = dto.Level,
                PersonId = dto.PersonId,
                Status = dto.Status,
                SectionId = dto.SectionId
            };
        }

        public void AddStudent(IStudent dto)
        {
            Student student = MapDtoToEntity(dto);
            student.CreatedDate = DateTime.Now;
            Service<PersonService>(service =>
            {
                student.Person = service.MapDtoToPersonEntity(dto);
                student.Person.ContactInfo = service.MapDtoToContactInfoEntity(dto);
            });
            Repository<Student>(repo => repo.Add(student).Save());
        }

        public void UpdateStudent(IStudent dto)
        {
            Service<PersonService>(service =>
            {
                service.UpdatePersonalInfo(dto);
                service.UpdateContactInfo(dto);
            });
            Repository<Student>(repo => repo.Update(MapDtoToEntity(dto), x => x.CreatedDate).Save());
        }


        internal IQueryable<IStudent> Students()
        {
            return Query(context =>
            {
                return (from a in context.Students
                        join b in context.Persons
                        on a.PersonId equals b.Id
                        join c in context.ContactInfo
                        on b.ContactInfoId equals c.Id
                        select new StudentDto
                        {
                            PersonId = b.Id,
                            FirstName = b.FirstName,
                            LastName = b.LastName,
                            BirthDate = b.BirthDate,
                            Gender = b.Gender,
                            ContactInfoId = c.Id,
                            Email = c.Email,
                            Telephone = c.Telephone,
                            Mobile = c.Mobile,
                            Id = a.Id,
                            CourseId = a.CourseId,
                            Course = a.Course.Code,
                            Level = a.Level,
                            CreatedDate = a.CreatedDate,
                            Status = a.Status,
                            SectionId = a.SectionId,
                            SectionCode = a.Section.Code
                        });
            });
        }

        public IStudent GetStudent(int id)
        {
            return Query(context => Students().SingleOrDefault(o => o.Id == id));
        }

        public IEnumerable<IStudent> GetAlltudents()
        {
            return Students().OrderBy(o => o.Status).ThenBy(o => o.FirstName).ThenBy(o => o.LastName).ToList();
        }

        public IEnumerable<IStudent> GetStudentsByCourseId(int id)
        {
            return Students().Where(o => o.CourseId == id && o.Status == Status.Active)
                .OrderBy(o => o.FirstName).ThenBy(o => o.LastName).ToList();
        }

        public IEnumerable<IStudent> GetStudentsByClassId(int id)
        {
            return Query(context =>
            {
                return (from a in context.StudentClassMapping
                        join b in Students()
                        on a.StudentId equals b.Id
                        where a.ClassId == id && b.Status == Status.Active
                        select b).ToList();
            });
        }
    }
}
