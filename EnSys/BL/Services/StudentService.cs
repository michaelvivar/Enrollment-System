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

namespace BL.Services
{
    public class StudentService : PersonService, IService
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
            };
        }

        public void AddStudent(IStudent dto)
        {
            Repository<Student>(repo =>
            {
                Student student = MapDtoToEntity(dto);
                student.CreatedDate = DateTime.Now;
                student.Status = Status.Active;
                student.Person = MapDtoToPersonEntity(dto);
                student.Person.ContactInfo = MapDtoToContactInfoEntity(dto);
                repo.Add(student);
            });
        }

        public void UpdateStudent(IStudent dto)
        {
            Repository<Student>(repo =>
            {
                Student student = MapDtoToEntity(dto);
                repo.Update(student, "Status", "CreatedDate");
                UpdatePersonalInfo(dto);
                UpdateContactInfo(dto);
            });
        }

        public IStudent GetStudentById(int id)
        {
            return Query(context =>
            {
                return Students().SingleOrDefault(o => o.Id == id);
            });
        }

        public IEnumerable<IStudent> GetAllActiveStudents()
        {
            return Students().Where(o => o.Status == Status.Active)
                .OrderBy(o => o.FirstName).ThenBy(o => o.LastName).ToList();
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
