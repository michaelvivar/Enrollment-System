using BL.Dto;
using DL;
using DL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using Util.Enums;

namespace BL.Services
{
    public class StudentService : BaseService, IService
    {
        internal StudentService(Context context) : base(context) { }

        internal Student MapDtoToEntity(IStudent dto)
        {
            return new Student
            {
                Id = dto.Id,
                CourseId = (int)dto.CourseId,
                Level = (YearLevel)dto.Level,
                PersonId = dto.PersonId,
                Status = (Status)dto.Status,
                SectionId = (int)dto.SectionId
            };
        }

        public void AddStudent(IStudent dto)
        {
            Student student = MapDtoToEntity(dto);
            student.CreatedDate = DateTime.Now;
            Service<PersonService>(service =>
            {
                student.Person = service.MapDtoToPersonEntity(dto);
                student.Person.ContactInfo = service.MapDtoToContactInfoEntity(dto);
            });
            Repository<Student>(repo => repo.Add(student).Save());
        }

        public void UpdateStudent(IStudent dto)
        {
            Service<PersonService>(service =>
            {
                service.UpdatePersonalInfo(dto);
                service.UpdateContactInfo(dto);
            });
            Repository<Student>(repo => repo.Update(MapDtoToEntity(dto), x => x.CreatedDate).Save());
        }

        public void DeleteStudent(int id)
        {
            Repository<Student>(repo => repo.Remove(repo.Get(id)).Save());
        }


        internal IQueryable<IStudent> Students()
        {
            return Query(context =>
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
                            Status = a.Status,
                            SectionId = a.SectionId,
                            SectionCode = a.Section.Code
                        });
            });
        }

        public IStudent GetStudent(int id)
        {
            return Query(context => Students().SingleOrDefault(o => o.Id == id));
        }

        public IEnumerable<IStudent> GetStudents()
        {
            return Students().OrderBy(o => o.Status).ThenBy(o => o.FirstName).ThenBy(o => o.LastName).ToList();
        }

        public IEnumerable<IStudent> GetStudentsFiltered(int level, int section, int course, int status)
        {
            return Students()
                .Where(o => (level == 0 ? true : o.Level == (YearLevel)level) &&
                (section == 0 ? true : o.SectionId == section) &&
                (course == 0 ? true : o.CourseId == course) &&
                (status == 0 ? true : o.Status == (Status)status))
                .OrderBy(o => o.Status).ThenBy(o => o.FirstName).ThenBy(o => o.LastName).ToList();
        }

        public IEnumerable<IStudent> GetStudentsByCourseId(int id)
        {
            return Students().Where(o => o.CourseId == id && o.Status == Status.Active)
                .OrderBy(o => o.FirstName).ThenBy(o => o.LastName).ToList();
        }

        public IEnumerable<IStudent> GetStudentsByClassId(int id)
        {
            return Query(context =>
            {
                return (from a in Students()
                        join b in context.Classes
                        on a.SectionId equals b.SectionId
                        where b.Id == id && a.Status == Status.Active
                        select a).ToList();
            });
        }
    }
}
