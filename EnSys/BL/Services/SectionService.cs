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
    public class SectionService : BaseService, IService
    {
        internal SectionService(Context context) : base(context) { }

        internal Section MapDtoToEntity(ISection dto)
        {
            return new Section
            {
                Id = dto.Id,
                Code = dto.Code,
                Level = dto.Level,
                Remarks = dto.Remarks,
                Status = dto.Status
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
                        select new SectionDto
                        {
                            Id = a.Id,
                            Code = a.Code,
                            Level = a.Level,
                            Remarks = a.Remarks,
                            Status = a.Status
                        });
            });
        }

        public ISection GetSectionById(int id)
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
    }
}
