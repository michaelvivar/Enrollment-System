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
    public abstract class PersonService : BaseService
    {
        internal PersonService(Context context) : base(context) { }

        protected Person MapDtoToPersonEntity(IPersonalInfo dto)
        {
            return new Person
            {
                Id = dto.PersonId,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                BirthDate = dto.BirthDate,
                Gender = dto.Gender,
                ContactInfoId = dto.ContactInfoId
            };
        }

        protected IPersonalInfo MapEntityToPersonInfo(Person entity)
        {
            return new PersonDto();
        }

        protected ContactInfo MapDtoToContactInfoEntity(IContactInfo dto)
        {
            return new ContactInfo
            {
                Id = dto.ContactInfoId,
                Email = dto.Email,
                Telephone = dto.Telephone,
                Mobile = dto.Mobile,
            };
        }

        protected IContactInfo MapEntityToContactInfo(ContactInfo entity)
        {
            return new ContactInfoDto();
        }

        protected void UpdatePersonalInfo(IPersonalInfo dto)
        {
            Repository<Person>(repo => repo.Update(MapDtoToPersonEntity(dto)));
        }

        protected void UpdateContactInfo(IContactInfo dto)
        {
            Repository<ContactInfo>(repo => repo.Update(MapDtoToContactInfoEntity(dto)));
        }
    }

    public class PersonValidatorService : BaseService, IService
    {
        public PersonValidatorService(Context context) : base(context) { }
    }
}
