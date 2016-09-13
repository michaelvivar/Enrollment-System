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

        public void SendEmail(string title, string content, params string[] recipients)
        {
            Transaction.Service<EmailService>(service => service.Send(title, content, recipients));
        }

        public void SendSMS(string content, params string[] recipients)
        {

        }

        public void Dispose()
        {
            
        }
    }
}
