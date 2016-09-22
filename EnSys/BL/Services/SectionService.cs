using BL.Dto;
using DL;
using DL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using Util.Enums;

namespace BL.Services
{
    public class SectionService : BaseService, IService
    {
        internal SectionService(Context context) : base(context) { }

        internal Section MapDtoToEntity(ISection dto)
        {
            return new Section
            {
                Id = dto.Id,
                Code = dto.Code,
                Level = (YearLevel)dto.Level,
                Remarks = dto.Remarks,
                Status = (Status)dto.Status
            };
        }


        public void AddSection(ISection dto)
        {
            Repository<Section>(repo => repo.Add(MapDtoToEntity(dto)));
        }

        public void UpdateSection(ISection dto)
        {
            Repository<Section>(repo => repo.Update(MapDtoToEntity(dto)));
        }

        public bool DeleteSection(int id)
        {
            if (Repository<ClassSchedule, bool>(repo => repo.Get(o => o.SectionId == id).Any()))
                return false;

            Repository<Section>(repo => repo.Remove(repo.Get(id)));
            return true;
        }


        internal IQueryable<ISection> Sections()
        {
            return Query(context =>
            {
                return (from a in context.Sections
                        let count = context.Students.Where(o => o.SectionId == a.Id).Count()
                        select new SectionDto
                        {
                            Id = a.Id,
                            Code = a.Code,
                            Level = a.Level,
                            Remarks = a.Remarks,
                            Status = a.Status,
                            Students = count
                        });
            });
        }

        public ISection GetSection(int id)
        {
            return Sections().Where(o => o.Id == id).FirstOrDefault();
        }

        public IEnumerable<ISection> GetActiveSections()
        {
            return Sections().Where(o => o.Status == Status.Active).OrderBy(o => o.Level).ThenBy(o => o.Code).ToList();
        }

        public IEnumerable<ISection> GetSections()
        {
            return Sections().OrderBy(o => o.Level).ThenBy(o => o.Code).ToList();
        }

        public IEnumerable<IOption> GetRecordsBindToDropDown(int? id)
        {
            if (id.HasValue)
            {
                YearLevel level = (YearLevel)id;
                return Sections().Where(o => o.Status == Status.Active && o.Level == level)
                    .OrderBy(o => o.Level).ThenBy(o => o.Code)
                    .Select(o => new OptionDto { Text = o.Code, Value = o.Id }).ToList();
            }

            return Sections().Where(o => o.Status == Status.Active)
                .OrderBy(o => o.Level).ThenBy(o => o.Code)
                .Select(o => new OptionDto { Text = o.Code, Value = o.Id }).ToList();
        }

        public IEnumerable<IOption> GetRecordsBindToDropDownBySubjectId(int id)
        {
            return Query(context =>
            {
                return (from a in Sections()
                        let subject = context.Subjects.FirstOrDefault(o => o.Id == id)
                        orderby a.Code
                        where a.Level == subject.Level
                        select new OptionDto
                        {
                            Text = a.Code,
                            Value = a.Id
                        }).ToList();
            });
        }
    }

    public class SectionValidatorService : ValidatorService, IService
    {
        internal SectionValidatorService(Context context) : base(context) { }

        public bool CheckSecionCodeExists(int id, string code)
        {
            return Query(context => context.Sections.Where(o => o.Code == code && ((id == 0) ? true : o.Id != id)).Any());
        }

        public bool CheckSectionAvailability(int classId, int? sectionId, DateTime start, DateTime end, DayOfWeek day)
        {
            bool available = true;
            int timeStart = start.Hour * 100 + start.Minute;
            int timeEnd = end.Hour * 100 + end.Minute;
            var records = Query(context =>
            {
                return (from a in context.Classes where a.SectionId == sectionId && a.Day == day && a.Id != classId select new { a.TimeStart, a.TimeEnd }).ToList();
            });
            records.ForEach(o =>
            {
                int a = o.TimeStart.Hour * 100 + o.TimeStart.Minute;
                int b = o.TimeEnd.Hour * 100 + o.TimeEnd.Minute;
                if (timeStart > a && timeStart < b)
                    available = false;

                if (timeEnd > a && timeEnd < b)
                    available = false;

                if (timeStart == a && timeEnd == b)
                    available = false;
            });

            return available;
        }
    }
}
