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
    public class StudentService : PersonService, IService
    {
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
                    student.Person = MapDtoToPersonEntity(dto);
                    student.Person.ContactInfo = MapDtoToContactInfoEntity(dto);
                    repo.Add(student);
                });
            });
        }

        public void UpdateStudent(StudentDto dto)
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

        public void UpdateStudentPersonalInfo(StudentDto dto)
        {
            UpdatePersonalInfo(MapDtoToPersonEntity(dto));
        }

        public void UpdateStudentContactInfo(StudentDto dto)
        {
            UpdateContactInfo(MapDtoToContactInfoEntity(dto));
        }
    }
}
