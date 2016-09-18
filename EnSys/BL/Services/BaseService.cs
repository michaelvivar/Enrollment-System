using DL;
using System;

namespace BL.Services
{
    public abstract class BaseService
    {
        private Context _context { get; set; }
        internal BaseService(Context context) { _context = context; }
        public virtual void Dispose()
        {

        }


        protected void Service<TService>(Action<TService> action) where TService : IService
        {
            Transaction.Service(_context, action);
        }
        protected TOut Service<TService, TOut>(Func<TService, TOut> action) where TService : IService
        {
            return Transaction.Service(_context, action);
        }
        protected TOut Query<TOut>(Func<Context, TOut> action)
        {
            return action.Invoke(_context);
        }
        protected void Repository<TEntity>(Action<IRepository<TEntity>> action) where TEntity : class
        {
            action.Invoke(new Repository<TEntity>(_context));
        }
        protected TOut Repository<TEntity, TOut>(Func<IRepository<TEntity>, TOut> action) where TEntity : class
        {
            return action.Invoke(new Repository<TEntity>(_context));
        }

    }

    public interface IService : IDisposable
    {

    }
}
