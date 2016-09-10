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

        protected Person MapDtoToPersonEntity(IPersonalInfo dto)
        {
            return new Person();
        }

        protected IPersonalInfo MapEntityToPersonInfo(Person entity)
        {
            return new PersonDto();
        }

        protected ContactInfo MapDtoToContactInfoEntity(IContactInfo dto)
        {
            return new ContactInfo();
        }

        protected IContactInfo MapEntityToContactInfo(ContactInfo entity)
        {
            return new ContactInfoDto();
        }
    }
}
