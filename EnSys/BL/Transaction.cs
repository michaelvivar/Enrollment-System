using BL.Services;
using DL;
using System;
using System.Linq;
using System.Reflection;

namespace BL
{
    public class Transaction : ITransaction, IDisposable
    {
        private readonly Context _context;
        private Transaction(Context context) { _context = context; }

        public void Service<TService>(Action<TService> action) where TService : IService
        {
            Service(_context, action);
        }

        public TOut Service<TService, TOut>(Func<TService, TOut> action) where TService : IService
        {
            return Service(_context, action);
        }

        public void Dispose()
        {
            if (_context.ChangeTracker.HasChanges())
                _context.SaveChanges();
            _context.Dispose();
        }


        #region Static Members
        private static Context Context()
        {
            return (Context)Activator.CreateInstance(typeof(Context), BindingFlags.NonPublic | BindingFlags.Instance, null, null, null, null);
        }

        public static void Scope(Action<ITransaction> action)
        {
            using (Transaction transaction = new Transaction(Context()))
            {
                action.Invoke(transaction);
            }
        }

        public static TOut Scope<TOut>(Func<ITransaction, TOut> action)
        {
            using (Transaction transaction = new Transaction(Context()))
            {
                return action.Invoke(transaction);
            }
        }

        internal static void Service<TService>(Context context, Action<TService> action) where TService : IService
        {
            Service<TService, TService>(context, service =>
            {
                action.Invoke(service);
                return service;
            });
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
        #endregion
    }

    public interface ITransaction
    {
        void Service<TService>(Action<TService> action) where TService : IService;

        TOut Service<TService, TOut>(Func<TService, TOut> action) where TService : IService;
    }
}
