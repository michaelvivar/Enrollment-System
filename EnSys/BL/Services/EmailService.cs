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
    public class EmailService : BaseService, IService
    {
        internal EmailService(Context context) : base(context) { }

        public void Send(string title, string content, params string[] recipients)
        {

        }
    }
}
