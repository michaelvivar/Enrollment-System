using BL.Dto;
using BL.Interfaces;
using DL;
using DL.Entities;
using System.Collections.Generic;
using System.Linq;
using Util.Enums;

namespace BL.Services
{
    public class InstructorService : PersonService, IService
    {
        private Instructor MapDtoToEntity(IInstructor dto)
        {
            return new Instructor();
        }

        private IInstructor MapEntityToDto(Instructor entity)
        {
            return new InstructorDto();
        }

        public void AddInstructor(IInstructor dto)
        {
            Db.UnitOfWork(uow =>
            {
                uow.Repository<Instructor>(repo =>
                {
                    Instructor teacher = MapDtoToEntity(dto);
                    teacher.Person = MapDtoToPersonEntity(dto);
                    teacher.Person.ContactInfo = MapDtoToContactInfoEntity(dto);
                    repo.Add(teacher);
                });
            });
        }

        public void UpdateInstructor(IInstructor dto)
        {
            Db.UnitOfWork(uow =>
            {
                uow.Repository<Instructor>(repo => repo.Update(MapDtoToEntity(dto)));
            });
        }

        public void ActivateInstructor(int id)
        {
            Db.UnitOfWork(uow => uow.Repository<Instructor>(repo =>
            {
                Instructor teacher = repo.Get(id);
                teacher.Status = Status.Active;
                repo.Update(teacher);
            }));
        }

        public void InactivateInstructor(int id)
        {
            Db.UnitOfWork(uow => uow.Repository<Instructor>(repo =>
            {
                Instructor teacher = repo.Get(id);
                teacher.Status = Status.Inactive;
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

        internal IQueryable<IInstructor> AllInstructors()
        {
            return Db.Context(context =>
            {
                return (from a in context.Instrcutors
                        join b in context.Persons
                        on a.PersonId equals b.Id
                        join c in context.ContactInfo
                        on b.ContactInfoId equals c.Id
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
                        });
            });
        }

        public IEnumerable<IInstructor> GetAllActiveInstructors()
        {
            return AllInstructors().Where(o => o.Status == Status.Active)
                .OrderBy(o => o.FirstName).ThenBy(o => o.LastName).ToList();
        }

        public IEnumerable<IDropDownMenuITem> GetRecordsBindToDropDown()
        {
            return AllInstructors().Where(o => o.Status == Status.Active)
                .OrderBy(o => o.FirstName).ThenBy(o => o.LastName)
                .Select(o => new DropDownMenuItemDto { Text = o.FirstName + " " + o.LastName, Value = o.Id }).ToList();
        }
    }
}
