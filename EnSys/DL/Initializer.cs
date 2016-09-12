using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util.Enums;

namespace DL
{
    public class Initializer : DropCreateDatabaseAlways<Context>
    {
        protected override void Seed(Context context)
        {
            context.Options.Add(new Entities.Option { Type = OptionType.Status, Text = "Active", Value = (int)Status.Active });
            context.Options.Add(new Entities.Option { Type = OptionType.Status, Text = "Inactive", Value = (int)Status.Inactive });

            context.Options.Add(new Entities.Option { Type = OptionType.Gender, Text = "Male", Value = (int)Gender.Male });
            context.Options.Add(new Entities.Option { Type = OptionType.Gender, Text = "Female", Value = (int)Gender.Female });

            context.Options.Add(new Entities.Option { Type = OptionType.YearLevel, Text = "1st Year", Value = (int)YearLevel.First });
            context.Options.Add(new Entities.Option { Type = OptionType.YearLevel, Text = "2nd Year", Value = (int)YearLevel.Second });
            context.Options.Add(new Entities.Option { Type = OptionType.YearLevel, Text = "3rd Year", Value = (int)YearLevel.Third });
            context.Options.Add(new Entities.Option { Type = OptionType.YearLevel, Text = "4th Year", Value = (int)YearLevel.Fourth });

            base.Seed(context);
        }
    }
}
