﻿using BL.Dto;
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

        public void DeleteSection(int id)
        {
            Repository<Section>(repo =>
            {
                Section entity = repo.Get(id);
                repo.Remove(entity);
            });
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

    public class SectionValidatorService : BaseService, IService
    {
        internal SectionValidatorService(Context context) : base(context) { }

        public bool CheckSecionCodeExists(int id, string code)
        {
            var record = Query(context => context.Sections.Where(o => o.Code == code).Select(o => o.Id)).FirstOrDefault();
            if (record != 0)
            {
                if (id == 0)
                    return true;

                if (record == id)
                    return false;

                return true;
            }
            return false;
        }
    }
}
