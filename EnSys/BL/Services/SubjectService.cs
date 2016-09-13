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
        internal SubjectService(Context context) : base(context) { }

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
            Repository<Subject>(repo => repo.Add(MapDtoToEntity(dto)));
        }

        public void UpdateSubject(ISubject dto)
        {
            Repository<Subject>(repo => repo.Update(MapDtoToEntity(dto)));
        }

        public void DeleteSubject(int id)
        {
            Repository<Subject>(repo =>
            {
                Subject subject = repo.Get(id);
                repo.Remove(subject);
            });
        }

        public void ActivateSubject(int id)
        {
            Repository<Subject>(repo =>
            {
                Subject entity = repo.Get(id);
                entity.Status = Status.Active;
                repo.Update(entity);
            });
        }

        public void InactivateSubject(int id)
        {
            Repository<Subject>(repo =>
            {
                Subject entity = repo.Get(id);
                entity.Status = Status.Inactive;
                repo.Update(entity);
            });
        }

        public ISubject GetSubjectById(int id)
        {
            return Subjects().Where(o => o.Id == id).FirstOrDefault();
        }

        public IEnumerable<ISubject> GetSubjectByCourseId(int id)
        {
            return Query(context =>
            {
                return (from a in context.CourseSubjectMapping
                        join b in Subjects()
                        on a.SubjectId equals b.Id
                        where a.CourseId == id && b.Status == Status.Active
                        orderby a.Level
                        select b).ToList();
            });
        }

        public IEnumerable<IOption> GetRecordsBindToDropDown()
        {
            return Subjects().Where(o => o.Status == Status.Active)
                .OrderBy(o => o.Level).ThenBy(o => o.Code)
                .Select(o => new OptionDto { Group = o.Level, Text = o.Code, Value = o.Id }).ToList();
        }
    }
}
