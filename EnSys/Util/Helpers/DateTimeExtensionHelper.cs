using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Util.Helpers
{
    public static class DateTimeExtensionHelper
    {
        public static double Age(this DateTime birthdate)
        {
            var today = DateTime.Today;
            int age = today.Year - birthdate.Year;

            double x = (today.Month * 30.4) + today.Day;
            double y = (birthdate.Month * 30.4) + birthdate.Day;

            if ((y - x) == 1)
                return (age - 1) + 0.99;
            else if ((x - y) == 1)
                return age + 0.01;
            else if ((y - x) == 0)
                return age;

            return Math.Round(y > x ?
                ((1.0 / 365) * (365 - (y - x))) + (age - 1) :
                ((1.0 / 365) * (x - y)) + age, 2);

        }

        public static double Age(this DateTime? birthdate)
        {
            if (birthdate.HasValue)
            {
                return ((DateTime)birthdate).Age();
            }
            return 0;
        }
    }
}
