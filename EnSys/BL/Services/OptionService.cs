using BL.Interfaces;
using DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util.Enums;

namespace BL.Services
{
    public class OptionService : BaseService, IService
    {
        public OptionService(Context context) : base(context) { }

        public IEnumerable<IOption> GetRecordsBindToDropDown(OptionType type)
        {
            return Query(context => Options().Where(o => o.Type == type).ToList());
        }
    }
}
