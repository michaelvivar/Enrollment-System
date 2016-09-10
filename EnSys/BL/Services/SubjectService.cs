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
    public class SubjectService : BaseService, IService
    {
        public void Dispose()
        {
            
        }

        private Subject MapDtoToEntity(ISubject dto)
        {
            return new Subject();
        }

        private ISubject MapEntityToDto(Subject entity)
        {
            return new SubjectDto();
        }

        public void AddSubject(ISubject dto)
        {
            Db.UnitOfWork(uow => uow.Repository<Subject>(repo => repo.Add(MapDtoToEntity(dto))));
        }

        public void UpdateSubject(ISubject dto)
        {
            Db.UnitOfWork(uow => uow.Repository<Subject>(repo => repo.Update(MapDtoToEntity(dto))));
        }

        public void DeleteSubject(int id)
        {
            Db.UnitOfWork(uow => uow.Repository<Subject>(repo =>
            {
                Subject subject = repo.Get(id);
                repo.Remove(subject);
            }));
        }

        public void ActivateSubject(int id)
        {
            Db.UnitOfWork(uow => uow.Repository<Subject>(repo =>
            {
                Subject entity = repo.Get(id);
                entity.Status = Status.Active;
                repo.Update(entity);
            }));
        }

        public void InactivateSubject(int id)
        {
            Db.UnitOfWork(uow => uow.Repository<Subject>(repo =>
            {
                Subject entity = repo.Get(id);
                entity.Status = Status.Inactive;
                repo.Update(entity);
            }));
        }

        public ISubject GetSubjectById(int id)
        {
            return Db.Context(context =>
            {
                return (from a in context.CourseSubjectMapping
                        join b in context.Subjects
                        on a.SubjectId equals b.Id
                        join c in context.Courses
                        on a.CourseId equals c.Id
                        where a.SubjectId == id
                        select new SubjectDto
                        {
                            Id = b.Id,
                            Code = b.Code,
                            Level = b.Level,
                            Remarks = b.Remarks,
                            Status = b.Status,
                            Units = b.Units,
                            Course = c.Code
                        }).FirstOrDefault();
            });
        }

        public IEnumerable<ISubject> GetSubjectByCourseId(int id)
        {
            return Db.Context(context =>
            {
                return (from a in context.CourseSubjectMapping
                        join b in context.Subjects
                        on a.SubjectId equals b.Id
                        join c in context.Courses
                        on a.CourseId equals c.Id
                        where a.CourseId == id && b.Status == Status.Active
                        orderby a.Level
                        select new SubjectDto
                        {
                            Id = b.Id,
                            Code = b.Code,
                            Level = b.Level,
                            Remarks = b.Remarks,
                            Status = b.Status,
                            Units = b.Units,
                            Course = c.Code
                        }).ToList();
            });
        }
    }
}
