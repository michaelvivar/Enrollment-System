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
            var a = (today.Year * 100 + today.Month) * 100 + today.Day;
            var b = (birthdate.Year * 100 + birthdate.Month) * 100 + birthdate.Day;

            return (a - b) / 10000;
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
