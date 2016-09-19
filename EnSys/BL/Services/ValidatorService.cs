using DL;
using System;
using System.Linq;

namespace BL.Services
{
    public class ValidatorService : BaseService, IService
    {
        internal ValidatorService(Context context) : base(context) { }

        public bool CheckPersonExists(int? id, string firstName, string lastName, DateTime? birthdate)
        {
            return Query(context => context.Persons.Where(
                o => o.FirstName == firstName && o.LastName == lastName && o.BirthDate == birthdate
                && ((id == 0) ? true : o.Id != id)).Any());
        }

        public bool CheckEmailExists(int? id, string email)
        {
            return Query(context => context.ContactInfo.Where(o => o.Email == email && ((id == 0) ? true : o.Id != id)).Any());
        }
    }
}
