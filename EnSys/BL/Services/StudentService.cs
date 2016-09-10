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
            return Db.Context(context =>
            {
                return (from a in context.Students
                        join b in context.Persons
                        on a.PersonId equals b.Id
                        join c in context.ContactInfo
                        on a.CourseId equals c.Id
                        where a.Status == Status.Active
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
                        }).ToList();
            });
        }

        public IEnumerable<IStudent> GetStudentsByCourseId(int id)
        {
            return Db.Context(context =>
            {
                return (from a in context.Students
                        join b in context.Persons
                        on a.PersonId equals b.Id
                        join c in context.ContactInfo
                        on a.CourseId equals c.Id
                        where a.Status == Status.Active && a.CourseId == id
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
                        }).ToList();
            });
        }

        public IEnumerable<IStudent> GetStudentsByClassId(int id)
        {
            return Db.Context(context =>
            {
                return (from a in context.StudentClassMapping
                        join b in context.Students
                        on a.StudentId equals b.Id
                        join c in context.Persons
                        on b.PersonId equals c.Id
                        join d in context.ContactInfo
                        on c.ContactInfoId equals d.Id
                        where a.ClassId == id
                        select new StudentDto
                        {
                            PersonId = b.Id,
                            FirstName = c.FirstName,
                            LastName = c.LastName,
                            BirthDate = c.BirthDate,
                            Gender = c.Gender,
                            ContactInfoId = c.Id,
                            Email = d.Email,
                            Telephone = d.Telephone,
                            Mobile = d.Mobile,
                            Id = a.Id,
                            CourseId = b.CourseId,
                            Course = b.Course.Code,
                            Level = b.Level,
                            CreatedDate = b.CreatedDate,
                            Status = b.Status
                        });
            });
        }
    }
}
