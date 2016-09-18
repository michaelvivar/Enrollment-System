using BL.Dto;
using DL;
using System.Collections.Generic;
using System.Linq;
using Util.Enums;

namespace BL.Services
{
    public class OptionService : BaseService, IService
    {
        internal OptionService(Context context) : base(context) { }

        internal IQueryable<IOption> Options()
        {
            return Query(context =>
            {
                return (from a in context.Options
                        select new OptionDto
                        {
                            Value = a.Value,
                            Text = a.Text,
                            Type = a.Type
                        });
            });
        }

        public IEnumerable<IOption> GetRecordsBindToDropDown(OptionType type)
        {
            return Query(context => Options().Where(o => o.Type == type).ToList());
        }
    }
}
