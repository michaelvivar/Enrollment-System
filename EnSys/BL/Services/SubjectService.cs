using BL.Dto;
using DL;
using DL.Entities;
using System.Collections.Generic;
using System.Linq;
using Util.Enums;

namespace BL.Services
{
    public class SubjectService : BaseService, IService
    {
        internal SubjectService(Context context) : base(context) { }

        internal Subject MapDtoToEntity(ISubject dto)
        {
            return new Subject
            {
                Id = dto.Id,
                Code = dto.Code,
                Level = (YearLevel)dto.Level,
                Units = (Unit)dto.Units,
                Remarks = dto.Remarks,
                Status = (Status)dto.Status
            };
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


        internal IQueryable<ISubject> Subjects()
        {
            return Query(context =>
            {
                return (from a in context.Subjects
                        let level = context.Options.Where(o => o.Value == (int)a.Level).FirstOrDefault()
                        let status = context.Options.Where(o => o.Value == (int)a.Status).FirstOrDefault()
                        select new SubjectDto
                        {
                            Id = a.Id,
                            Code = a.Code,
                            Level = a.Level,
                            Remarks = a.Remarks,
                            Status = a.Status,
                            Units = a.Units
                        });
            });
        }

        public ISubject GetSubject(int id)
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
                        orderby b.Code
                        select b).ToList();
            });
        }

        public IEnumerable<ISubject> GetActiveSubjects()
        {
            return Subjects().Where(o => o.Status == Status.Active).OrderBy(o => o.Level).ThenBy(o => o.Code).ToList();
        }

        public IEnumerable<ISubject> GetSubjects()
        {
            return Subjects().OrderBy(o => o.Level).ThenBy(o => o.Code).ToList();
        }

        public IEnumerable<IOption> GetRecordsBindToDropDown()
        {
            return Subjects().Where(o => o.Status == Status.Active)
                .OrderBy(o => o.Level).ThenBy(o => o.Code)
                .Select(o => new OptionDto { Text = o.Code, Value = o.Id }).ToList();
        }
    }

    public class SubjectValidatorService : ValidatorService, IService
    {
        internal SubjectValidatorService(Context context) : base(context) { }

        public bool CheckSubjectCodeExists(int id, string code)
        {
            return Query(context => context.Subjects.Where(o => o.Code == code && ((id == 0) ? true : o.Id != id)).Any());
        }
    }
}
