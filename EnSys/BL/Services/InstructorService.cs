using BL.Dto;
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
    public class InstructorService : BaseService, IService
    {
        public void Dispose()
        {
            
        }

        private Instrcutor MapDtoToEntity(InstructorDto dto)
        {
            return new Instrcutor();
        }

        private InstructorDto MapEntityToDto(Instrcutor entity)
        {
            return new InstructorDto();
        }

        public void AddTeacher(InstructorDto dto)
        {
            Db.UnitOfWork(uow =>
            {
                uow.Repository<Instrcutor>(repo =>
                {
                    Instrcutor teacher = MapDtoToEntity(dto);
                    teacher.Person = Service<PersonService, Person>(service =>
                    {
                        Person person = service.MapDtoToEntity(dto);
                        service.AddPerson(person);
                        return person;
                    });
                    repo.Add(teacher);
                });
            });
        }

        public void UpdateTeacher(InstructorDto dto)
        {
            Db.UnitOfWork(uow =>
            {
                Service<PersonService>(service => service.UpdatePerson(service.MapDtoToEntity(dto)));
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
    }
}
