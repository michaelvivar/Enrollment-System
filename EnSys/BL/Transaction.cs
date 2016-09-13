using BL.Services;
using DL;
using System;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using Util.Helpers;

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
            Context context = new Context();
            var o = Service(context, action);
            if (context.ChangeTracker.HasChanges())
                context.SaveChanges();
            context.SaveChanges();
            return o;
        }

        internal static TOut Service<TService, TOut>(Context context, Func<TService, TOut> action) where TService : IService
        {
            BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
            CultureInfo culture = null;
            Type type = typeof(TService);
            try
            {
                if (type.GetConstructors(flags).All(c => c.GetParameters().Length == 0))
                {
                    TService service = (TService)Activator.CreateInstance(type, flags, null, null, culture, null);
                    var o = action.Invoke(service);
                    service.Dispose();
                    return o;
                }
                else
                {
                    TService service = (TService)Activator.CreateInstance(type, flags, null, new object[] { context }, culture, null);
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
}

//public static TOut Service<TService, TOut>(Func<TService, TOut> action) where TService : IService
//{
//    //TService service = (TService)FormatterServices.GetUninitializedObject(typeof(TService));
//    //TService service = (TService)Activator.CreateInstance(typeof(TService), System.Reflection.BindingFlags.NonPublic, null, context, null); 

//    try
//    {
//        Type type = typeof(TService);
//        if (type.GetConstructors().All(c => c.GetParameters().Length == 0))
//        {
//            TService service = (TService)Activator.CreateInstance(type);
//            var o = action.Invoke(service);
//            service.Dispose();
//            return o;
//        }
//        else
//        {
//            Context context = new Context();
//            TService service = (TService)Activator.CreateInstance(type, context);
//            var o = action.Invoke(service);
//            if (context.ChangeTracker.HasChanges()) { context.SaveChanges(); }
//            context.Dispose();
//            return o;
//        }
//    }
//    catch (Exception e)
//    {
//        throw e;
//    }
//}