using BL.Dto;
using DL;
using DL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public class StudentService : BaseService, IService
    {
        public void Dispose()
        {
            
        }

        private Student MapDtoToEntity(StudentDto dto)
        {
            return new Student();
        }

        private StudentDto MapEntityToDto(Student entity)
        {
            return new StudentDto();
        }

        public void AddStudent(StudentDto dto)
        {
            Db.UnitOfWork(uow =>
            {
                uow.Repository<Student>(repo =>
                {
                    Student student = MapDtoToEntity(dto);
                    student.Person = Service<PersonService, Person>(service =>
                    {
                        Person person = service.MapDtoToEntity(dto);
                        service.AddPerson(person);
                        return person;
                    });
                    repo.Add(student);
                });
            });
        }

        public void UpdateStudent(StudentDto dto)
        {
            Db.UnitOfWork(uow =>
            {
                Service<PersonService>(service => service.UpdatePerson(service.MapDtoToEntity(dto)));
                uow.Repository<Student>(repo => repo.Update(MapDtoToEntity(dto)));
            });
        }
    }
}
