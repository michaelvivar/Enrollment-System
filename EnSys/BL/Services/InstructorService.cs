using BL.Dto;
using BL.Interfaces;
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
    public class InstructorService : PersonService, IService
    {
        private Instrcutor MapDtoToEntity(IInstructor dto)
        {
            return new Instrcutor();
        }

        private IInstructor MapEntityToDto(Instrcutor entity)
        {
            return new InstructorDto();
        }

        public void AddTeacher(IInstructor dto)
        {
            Db.UnitOfWork(uow =>
            {
                uow.Repository<Instrcutor>(repo =>
                {
                    Instrcutor teacher = MapDtoToEntity(dto);
                    teacher.Person = MapDtoToPersonEntity(dto);
                    teacher.Person.ContactInfo = MapDtoToContactInfoEntity(dto);
                    repo.Add(teacher);
                });
            });
        }

        public void UpdateTeacher(IInstructor dto)
        {
            Db.UnitOfWork(uow =>
            {
                uow.Repository<Instrcutor>(repo => repo.Update(MapDtoToEntity(dto)));
            });
        }

        public void ActivateTeacher(int id)
        {
            Db.UnitOfWork(uow => uow.Repository<Instrcutor>(repo =>
            {
                Instrcutor teacher = repo.Get(id);
                teacher.Status = Status.Active;
                repo.Update(teacher);
            }));
        }

        public void DeactivateTeacher(int id)
        {
            Db.UnitOfWork(uow => uow.Repository<Instrcutor>(repo =>
            {
                Instrcutor teacher = repo.Get(id);
                teacher.Status = Status.Deactive;
                repo.Update(teacher);
            }));
        }

        public IInstructor GetInstructorById(int id)
        {
            return Db.Context(context =>
            {
                return (from a in context.Instrcutors
                        join b in context.Persons
                        on a.PersonId equals b.Id
                        join c in context.ContactInfo
                        on b.ContactInfoId equals c.Id
                        where a.Id == id
                        select new InstructorDto
                        {
                            Id = a.Id,
                            PersonId = b.Id,
                            FirstName = b.FirstName,
                            LastName = b.LastName,
                            BirthDate = b.BirthDate,
                            Gender = b.Gender,
                            ContactInfoId = c.Id,
                            Email = c.Email,
                            Telephone = c.Telephone,
                            Mobile = c.Mobile
                        }).FirstOrDefault();
            });
        }

        public IEnumerable<IInstructor> GetAllActiveInstructors()
        {
            return Db.Context(context =>
            {
                return (from a in context.Instrcutors
                        join b in context.Persons
                        on a.PersonId equals b.Id
                        join c in context.ContactInfo
                        on b.ContactInfoId equals c.Id
                        where a.Status == Status.Active
                        select new InstructorDto
                        {
                            Id = a.Id,
                            PersonId = b.Id,
                            FirstName = b.FirstName,
                            LastName = b.LastName,
                            BirthDate = b.BirthDate,
                            Gender = b.Gender,
                            ContactInfoId = c.Id,
                            Email = c.Email,
                            Telephone = c.Telephone,
                            Mobile = c.Mobile
                        }).ToList();
            });
        }
    }
}
