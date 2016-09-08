using BL.Services;
using System;

namespace BL
{
    public static class Transaction
    {
        public static void Service<TService>(Action<TService> action) where TService : IService, new()
        {
            Service<TService, string>(service =>
            {
                action.Invoke(service);
                return string.Empty;
            });
        }

        public static TOut Service<TService, TOut>(Func<TService, TOut> action) where TService : IService, new()
        {
            using (TService service = new TService())
            {
                return action.Invoke(service);
            }
        }
    }
}
