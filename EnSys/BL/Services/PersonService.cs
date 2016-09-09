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
    internal class PersonService : IService
    {
        public Person MapDtoToEntity(PersonDto dto)
        {
            return new Person();
        }

        public Person AddPerson(Person person)
        {
            return Db.UnitOfWork(uow => uow.Repository<Person, Person>(repo =>
            {
                repo.Add(person);
                return person;
            }));
        }

        public void UpdatePerson(Person person)
        {
            Db.UnitOfWork(uow => uow.Repository<Person>(repo => repo.Update(person)));
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
