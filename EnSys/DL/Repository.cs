using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DL
{
    public class Repository<TEntity> : IRepository<TEntity>, IRepository_Save<TEntity> where TEntity : class
    {
        private DbContext Context { get; set; }
        public Repository(DbContext context)
        {
            Context = context;
        }

        public IRepository_Save<TEntity> Add(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
            return this;
        }

        public IRepository_Save<TEntity> AddRange(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().AddRange(entities);
            return this;
        }

        public IRepository_Save<TEntity> Update(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
            return this;
        }

        public IRepository_Save<TEntity> Update<T>(TEntity entity, Expression<Func<TEntity, T>> exclude)
        {
            var entry = Context.Entry(entity);
            entry.State = EntityState.Modified;
            var body = (MemberExpression)exclude.Body;
            entry.Property(body.Member.Name).IsModified = false;
            return this;
        }

        public IRepository_Save<TEntity> Remove(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
            return this;
        }

        public IRepository_Save<TEntity> RemoveRange(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().RemoveRange(entities);
            return this;
        }

        public TEntity Get(int id)
        {
            return Context.Set<TEntity>().Find(id);
        }

        public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().Where(predicate);
        }

        public TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().SingleOrDefault(predicate);
        }

        public void Save()
        {
            Context.SaveChanges();
        }
    }

    public interface IRepository<TEntity>
    {
        IRepository_Save<TEntity> Add(TEntity entity);
        IRepository_Save<TEntity> AddRange(IEnumerable<TEntity> entities);
        IRepository_Save<TEntity> Update(TEntity entity);
        IRepository_Save<TEntity> Update<T>(TEntity entity, Expression<Func<TEntity, T>> exclude);
        IRepository_Save<TEntity> Remove(TEntity entity);
        IRepository_Save<TEntity> RemoveRange(IEnumerable<TEntity> entities);
        TEntity Get(int id);
        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate);
        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate);
    }

    public interface IRepository_Save<TEntity>
    {
        void Save();
    }
}
