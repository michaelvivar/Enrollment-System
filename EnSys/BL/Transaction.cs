using BL.Services;
using DL;
using System;

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

        public static void Service<TService>(Context context, Action<TService> action) where TService : IService
        {
            Service<TService, string>(context, service =>
            {
                action.Invoke(service);
                return string.Empty;
            });
        }

        public static TOut Service<TService, TOut>(Func<TService, TOut> action) where TService : IService
        {
            Context context = new Context();
            var o = Service(context, action);
            if (context.ChangeTracker.HasChanges())
            {
                context.SaveChanges();
            }
            context.Dispose();
            return o;
        }

        public static TOut Service<TService, TOut>(Context context, Func<TService, TOut> action) where TService : IService
        {
            TService service = (TService)Activator.CreateInstance(typeof(TService), context);
            var o = action.Invoke(service);
            service.Dispose();
            return o;
        }
    }
}
