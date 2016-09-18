using DL;

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
