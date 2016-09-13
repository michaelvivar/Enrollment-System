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
        internal InstructorService(Context context) : base(context) { }

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
            Repository<Instructor>(repo =>
            {
                Instructor teacher = MapDtoToEntity(dto);
                teacher.Person = MapDtoToPersonEntity(dto);
                teacher.Person.ContactInfo = MapDtoToContactInfoEntity(dto);
                repo.Add(teacher);
            });
        }

        public void UpdateInstructor(IInstructor dto)
        {
            Repository<Instructor>(repo => repo.Update(MapDtoToEntity(dto)));
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

        public IEnumerable<IInstructor> GetAllActiveInstructors()
        {
            return Instructors().Where(o => o.Status == Status.Active)
                    .OrderBy(o => o.FirstName).ThenBy(o => o.LastName).ToList();
        }

        public IEnumerable<IOption> GetRecordsBindToDropDown()
        {
            return Instructors().OrderBy(o => o.FirstName).ThenBy(o => o.LastName)
                    .Select(o => new OptionDto { Text = o.FirstName + " " + o.LastName, Id = o.Id }).ToList();
        }
    }
}
