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

            double x = (today.Month * 30.5) + today.Day;
            double y = (birthdate.Month * 30.5) + birthdate.Day;

            return Math.Round(y > x ?
                ((1.0 / 365.0) * (365.0 - (y - x))) + (age - 1.0) :
                ((1.0 / 365.0) * (x - y)) + age, 2);

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
