using BL;
using BL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Util.Enums;

namespace UI.Helpers
{
    public static class DateTimeHelperExtension
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