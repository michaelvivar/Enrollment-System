using DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public class SampleService : BaseService, IService
    {
        public SampleService(Context context) : base(context) { }

        public void Dispose()
        {
            
        }
    }
}
