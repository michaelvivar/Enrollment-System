using BL.Services;
using DL;
using System;
using System.Runtime.Serialization;

namespace BL
{
    public static class Transaction
    {
        public static void Service<TService>(Action<TService> action) where TService : IService
        {
            Service<TService, string>(service =>
            {
                action.Invoke(service);
                return string.Empty;
            });
        }

        internal static void Service<TService>(Context context, Action<TService> action) where TService : IService
        {
            Service<TService, string>(context, service =>
            {
                action.Invoke(service);
                return string.Empty;
            });
        }

        public static TOut Service<TService, TOut>(Func<TService, TOut> action) where TService : IService
        {
            //TService service = (TService)Activator.CreateInstance(typeof(TService));
            TService service = (TService)FormatterServices.GetUninitializedObject(typeof(TService));
            var o = action.Invoke(service);
            service.Dispose();
            return o;
        }

        internal static TOut Service<TService, TOut>(Context context, Func<TService, TOut> action) where TService : IService
        {
            TService service = (TService)Activator.CreateInstance(typeof(TService), context);
            var o = action.Invoke(service);
            return o;
        }
    }
}
