using BL.Services;
using DL;
using System;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Reflection;

namespace BL
{
    public static class Transaction
    {
        private static Context Context()
        {
            return (Context)Activator.CreateInstance(typeof(Context), BindingFlags.NonPublic | BindingFlags.Instance, null, null, null, null);
        }

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
            Context context = Context();
            var o = Service(context, action);
            if (context.ChangeTracker.HasChanges())
            {
                context.SaveChanges();
            }
            context.Dispose();
            return o;
        }

        internal static TOut Service<TService, TOut>(Context context, Func<TService, TOut> action) where TService : IService
        {
            BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
            Type type = typeof(TService);
            try
            {
                if (type.GetConstructors(flags).All(c => c.GetParameters().Length == 0))
                {
                    TService service = (TService)Activator.CreateInstance(type, flags, null, null, null, null);
                    var o = action.Invoke(service);
                    service.Dispose();
                    return o;
                }
                else
                {
                    TService service = (TService)Activator.CreateInstance(type, flags, null, new object[] { context }, null, null);
                    var o = action.Invoke(service);
                    service.Dispose();
                    return o;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }

    public interface ITransaction
    {
        void Service<TService>(Action<TService> action) where TService : IService;

        TOut Service<TService, TOut>(Func<TService, TOut> action) where TService : IService;
    }
}
