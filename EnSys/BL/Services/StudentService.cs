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
        private Student MapDtoToEntity(IStudent dto)
        {
            return new Student();
        }

        private IStudent MapEntityToDto(Student entity)
        {
            return new StudentDto();
        }

        public void AddStudent(IStudent dto)
        {
            Db.UnitOfWork(uow =>
            {
                uow.Repository<Student>(repo =>
                {
                    Student student = MapDtoToEntity(dto);
                    student.Person = MapDtoToPersonEntity(dto);
                    student.Person.ContactInfo = MapDtoToContactInfoEntity(dto);
                    repo.Add(student);
                });
            });
        }

        public void UpdateStudent(IStudent dto)
        {
            Db.UnitOfWork(uow =>
            {
                uow.Repository<Student>(repo =>
                {
                    Student student = MapDtoToEntity(dto);
                    repo.Update(student);
                });
            });
        }

        internal IQueryable<IStudent> AllStudents()
        {
            return Db.Context(context =>
            {
                return (from a in context.Students
                        join b in context.Persons
                        on a.PersonId equals b.Id
                        join c in context.ContactInfo
                        on a.CourseId equals c.Id
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
                            Status = a.Status
                        });
            });
        }

        public IStudent GetStudentById(int id)
        {
            return Db.Context(context =>
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
                            Status = a.Status
                        }).FirstOrDefault();
            });
        }

        public IEnumerable<IStudent> GetAllActiveStudents()
        {
            return AllStudents().Where(o => o.Status == Status.Active)
                .OrderBy(o => o.FirstName).ThenBy(o => o.LastName).ToList();
        }

        public IEnumerable<IStudent> GetStudentsByCourseId(int id)
        {
            return AllStudents().Where(o => o.CourseId == id && o.Status == Status.Active)
                .OrderBy(o => o.FirstName).ThenBy(o => o.LastName).ToList();
        }

        public IEnumerable<IStudent> GetStudentsByClassId(int id)
        {
            return Db.Context(context =>
            {
                return (from a in context.StudentClassMapping
                        join b in AllStudents()
                        on a.StudentId equals b.Id
                        where a.ClassId == id
                        select b).ToList();
            });
        }
    }
}
