﻿using BL.Services;
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
        private static Context getContext()
        {
            return (Context)Activator.CreateInstance(typeof(Context), BindingFlags.NonPublic | BindingFlags.Instance, null, null, null, null);
        }

        private static TService getService<TService>(Context context)
        {
            BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
            Type type = typeof(TService);
            if (type.GetConstructors(flags).All(c => c.GetParameters().Length == 0))
            {
                return (TService)Activator.CreateInstance(type, flags, null, null, null, null);
            }
            else
            {
                return (TService)Activator.CreateInstance(type, flags, null, new object[] { context }, null, null);
            }
        }

        public static void Scope(Action<ITransaction> action)
        {
            using (Transaction transaction = new Transaction(getContext()))
            {
                action.Invoke(transaction);
            }
        }

        public static TOut Scope<TOut>(Func<ITransaction, TOut> action)
        {
            using (Transaction transaction = new Transaction(getContext()))
            {
                return action.Invoke(transaction);
            }
        }

        internal static void Service<TService>(Context context, Action<TService> action) where TService : IService
        {
            Service<TService, bool>(context, service =>
            {
                action.Invoke(service);
                return true;
            });
        }

        internal static TOut Service<TService, TOut>(Context context, Func<TService, TOut> action) where TService : IService
        {
            try
            {
                TService service = getService<TService>(context);
                var o = action.Invoke(service);
                service.Dispose();
                return o;
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
