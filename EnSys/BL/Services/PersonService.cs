using BL.Dto;
using DL;
using DL.Entities;
using System;
using Util.Enums;

namespace BL.Services
{
    public class PersonService : BaseService, IService
    {
        internal PersonService(Context context) : base(context) { }

        internal Person MapDtoToPersonEntity(IPersonalInfo dto)
        {
            return new Person
            {
                Id = dto.PersonId,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                BirthDate = (DateTime)dto.BirthDate,
                Gender = (Gender)dto.Gender,
                ContactInfoId = dto.ContactInfoId
            };
        }

        internal ContactInfo MapDtoToContactInfoEntity(IContactInfo dto)
        {
            return new ContactInfo
            {
                Id = dto.ContactInfoId,
                Email = dto.Email,
                Telephone = dto.Telephone,
                Mobile = dto.Mobile
            };
        }

        internal void UpdatePersonalInfo(IPersonalInfo dto)
        {
            Repository<Person>(repo => repo.Update(MapDtoToPersonEntity(dto)));
        }

        internal void UpdateContactInfo(IContactInfo dto)
        {
            Repository<ContactInfo>(repo => repo.Update(MapDtoToContactInfoEntity(dto)));
        }
    }

    public class PersonValidatorService : BaseService, IService
    {
        public PersonValidatorService(Context context) : base(context) { }
    }
}
