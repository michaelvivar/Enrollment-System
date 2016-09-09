using BL.Dto;
using BL.Interfaces;
using DL;
using DL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public abstract class PersonService : BaseService, IDisposable
    {
        public void Dispose()
        {
            
        }

        protected Person MapDtoToPersonEntity(IPersonInfo dto)
        {
            return new Person();
        }

        protected ContactInfo MapDtoToContactInfoEntity(IContactInfo dto)
        {
            return new ContactInfo();
        }

        protected void UpdatePersonalInfo(Person entity)
        {
            Db.UnitOfWork(uow => uow.Repository<Person>(repo => repo.Update(entity)));
        }

        protected void UpdateContactInfo(ContactInfo entity)
        {
            Db.UnitOfWork(uow => uow.Repository<ContactInfo>(repo => repo.Update(entity)));
        }
    }
}
