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
                BirthDate = dto.BirthDate,
                Gender = dto.Gender,
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
                Mobile = dto.Mobile,
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
