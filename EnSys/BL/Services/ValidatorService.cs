using DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public class ValidatorService : BaseService, IService
    {
        internal ValidatorService(Context context) : base(context) { }

        public bool CheckPersonExists(int id, string firstName, string lastName, DateTime birthdate)
        {
            var records = Query(context => context.Persons.Where(o => o.FirstName == firstName && o.LastName == lastName && o.BirthDate == birthdate).Select(o => o.Id));
            if (records.Count() > 0)
            {
                if (id == 0)
                    return true;

                if (records.Any(o => o == id))
                    return false;

                return true;
            }
            return false;
        }

        public bool CheckEmailExists(int id, string email)
        {
            var records = Query(context => context.ContactInfo.Where(o => o.Email == email).Select(o => o.Id));
            if (records.Count() > 0)
            {
                if (id == 0)
                    return true;

                if (records.Any(o => o == id))
                    return false;

                return true;
            }
            return false;
        }
    }
}
