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
    public class SubjectService : BaseService, IService
    {
        public void Dispose()
        {
            
        }

        private Subject MapDtoToEntity(SubjectDto dto)
        {
            return new Subject();
        }

        private SubjectDto MapEntityToDto(Subject entity)
        {
            return new SubjectDto();
        }

        public void AddSubject(SubjectDto dto)
        {
            Db.UnitOfWork(uow => uow.Repository<Subject>(repo => repo.Add(MapDtoToEntity(dto))));
        }

        public void UpdateSubject(SubjectDto dto)
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
    }
}
