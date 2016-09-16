using BL.Dto;
using BL.Interfaces;
using DL;
using DL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using Util.Enums;

namespace BL.Services
{
    public class InstructorService : BaseService, IService
    {
        internal InstructorService(Context context) : base(context) { }

        internal Instructor MapDtoToEntity(IInstructor dto)
        {
            return new Instructor
            {
                Id = dto.Id,
                Status = dto.Status,
                PersonId = dto.PersonId,
            };
        }


        public void AddInstructor(IInstructor dto)
        {
            Instructor teacher = MapDtoToEntity(dto);
            teacher.CreatedDate = DateTime.Now;
            Service<PersonService>(service =>
            {
                teacher.Person = service.MapDtoToPersonEntity(dto);
                teacher.Person.ContactInfo = service.MapDtoToContactInfoEntity(dto);
            });
            Repository<Instructor>(repo => repo.Add(teacher).Save());
        }

        public void UpdateInstructor(IInstructor dto)
        {
            Service<PersonService>(service =>
            {
                service.UpdatePersonalInfo(dto);
                service.UpdateContactInfo(dto);
            });
            Repository<Instructor>(repo => repo.Update(MapDtoToEntity(dto), x => x.CreatedDate).Save());
        }


        internal IQueryable<IInstructor> Instructors()
        {
            return Query(context =>
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
                            Mobile = c.Mobile,
                            Status = a.Status,
                            CreatedDate = a.CreatedDate
                        });
            });
        }

        public IInstructor GetInstructorById(int id)
        {
            return Instructors().Where(o => o.Id == id).FirstOrDefault();
        }

        public IEnumerable<IInstructor> GetAllInstructors()
        {
            return Instructors().OrderBy(o => o.FirstName).ThenBy(o => o.LastName).ToList();
        }

        public IEnumerable<IOption> GetRecordsBindToDropDown()
        {
            return Instructors().OrderBy(o => o.FirstName).ThenBy(o => o.LastName)
                    .Select(o => new OptionDto { Text = o.FirstName + " " + o.LastName, Id = o.Id }).ToList();
        }
    }
}
