using BL.Interfaces;
using DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public class NotificationService : IService
    {
        internal NotificationService() { }

        public void OnStudentAdded(object sender, IStudent student)
        {

        }

        public void Dispose()
        {
            
        }
    }
}
