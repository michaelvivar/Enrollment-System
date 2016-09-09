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

        public void AddStudent(IStudentWithPersonInfo dto)
        {
            Db.UnitOfWork(uow =>
            {
                uow.Repository<Student>(repo =>
                {
                    Student student = MapDtoToEntity((StudentDto)dto);
                    student.Person = MapDtoToPersonEntity(dto.PersonInfo);
                    student.Person.ContactInfo = MapDtoToContactInfoEntity(dto.ContactInfo);
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

        public void UpdateStudentPersonalInfo(IPersonInfo person)
        {
            UpdatePersonalInfo(MapDtoToPersonEntity(person));
        }

        public void UpdateStudentContactInfo(IContactInfo contact)
        {
            UpdateContactInfo(MapDtoToContactInfoEntity(contact));
        }

        public IEnumerable<IStudent> GetByClassId(int id)
        {
            return Db.Context(context =>
            {
                return (from a in context.Students
                        where a.Status == Status.Active
                        select new StudentDto
                        {
                            Id = a.Id,
                            FirstName = a.Person.FirstName,
                            LastName = a.Person.LastName,
                            Course = a.Course.Code
                        }).ToList();
            });
        }

        public override IPersonInfo GetPersonInfo(int id)
        {
            return Db.Context(context =>
            {
                return (from a in context.Persons
                        join b in context.Students
                        on a.Id equals b.Id
                        where b.Id == id
                        select new PersonDto
                        {
                            Id = a.Id,
                            FirstName = a.FirstName,
                            LastName = a.LastName,
                            BirthDate = a.BirthDate,
                            Gender = a.Gender
                        }).SingleOrDefault();
            });
        }

        public override IContactInfo GetContactInfo(int id)
        {
            return Db.Context(context =>
            {
                return (from a in context.ContactInfo
                        join b in context.Persons
                        on a.Id equals b.Id
                        join c in context.Students
                        on b.Id equals c.PersonId
                        where c.Id == id
                        select new ContactInfoDto
                        {
                            Id = a.Id,
                            Email = a.Email,
                            Telephone = a.Telephone,
                            Mobile = a.Mobile
                        }).SingleOrDefault();
            });
        }
    }
}
