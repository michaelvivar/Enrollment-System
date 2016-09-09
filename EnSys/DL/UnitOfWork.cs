using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DL
{
    public class UnitOfWork : IUnitOfWork
    {
        internal UnitOfWork(DbContext context)
        {
            Context = context;
        }

        private readonly DbContext Context;
        private List<RepositoryContainer> Container = new List<RepositoryContainer>();

        public void Repository<TEntity>(Action<IRepository<TEntity>> action) where TEntity : class
        {
            action.Invoke(GetRepository<TEntity>());
        }

        public TOut Repository<TEntity, TOut>(Func<IRepository<TEntity>, TOut> action) where TEntity : class
        {
            return action.Invoke(GetRepository<TEntity>());
        }

        private IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            if (Container.Any(o => o.Type != typeof(TEntity)))
            {
                IRepository<TEntity> repository = new Repository<TEntity>(Context);
                Container.Add(new RepositoryContainer(typeof(TEntity), repository));
                return repository;
            }
            return (IRepository<TEntity>)Container.Find(o => o.Type == typeof(TEntity)).Repository;
        }

        public void SaveChanges()
        {
            Context.SaveChanges();
        }
    }

    public interface IUnitOfWork
    {
        void Repository<TEntity>(Action<IRepository<TEntity>> action) where TEntity : class;
        TOut Repository<TEntity, TOut>(Func<IRepository<TEntity>, TOut> action) where TEntity : class;
        void SaveChanges();
    }

    internal class RepositoryContainer
    {
        internal RepositoryContainer(Type type, object repository)
        {
            Type = type;
            Repository = repository;
        }

        public readonly Type Type;
        public readonly object Repository;
    }
}
