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

        public event EventHandler<IStudent> Add_Student;

        private void OnStudentAdded(IStudent student)
        {
            if (Add_Student != null)
                Add_Student.Invoke(this, student);
        }

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
            Repository<Student>(repo =>
            {
                Student student = MapDtoToEntity(dto);
                student.CreatedDate = DateTime.Now;
                student.Person = MapDtoToPersonEntity(dto);
                student.Person.ContactInfo = MapDtoToContactInfoEntity(dto);
                repo.Add(student);
                repo.Save();
                dto.Id = student.Id;
                OnStudentAdded(dto);
            });
        }

        public void UpdateStudent(IStudent dto)
        {
            Repository<Student>(repo =>
            {
                Student student = MapDtoToEntity(dto);
                repo.Update(student, "CreatedDate");
                UpdatePersonalInfo(dto);
                UpdateContactInfo(dto);
            });
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
