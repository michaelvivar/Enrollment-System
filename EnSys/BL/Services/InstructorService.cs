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

        private Instructor MapDtoToEntity(IInstructor dto)
        {
            return new Instructor
            {
                Id = dto.Id,
                Status = dto.Status,
                PersonId = dto.PersonId,
            };
        }

        private IInstructor MapEntityToDto(Instructor entity)
        {
            return new InstructorDto();
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

        public void ActivateInstructor(int id)
        {
            Repository<Instructor>(repo =>
            {
                Instructor teacher = repo.Get(id);
                teacher.Status = Status.Active;
                repo.Update(teacher);
            });
        }

        public void InactivateInstructor(int id)
        {
            Repository<Instructor>(repo =>
            {
                Instructor teacher = repo.Get(id);
                teacher.Status = Status.Inactive;
                repo.Update(teacher);
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
