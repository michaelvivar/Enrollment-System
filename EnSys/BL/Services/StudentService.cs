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

        private Student MapDtoToEntity(IStudent dto)
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

        public IStudent GetStudentById(int id)
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
