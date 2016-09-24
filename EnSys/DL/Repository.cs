using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DL
{
    public class Repository<TEntity> : IRepository<TEntity>, IRepository where TEntity : class
    {
        private DbContext Context { get; set; }
        public Repository(DbContext context)
        {
            Context = context;
        }

        public IRepository Add(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
            return this;
        }

        public IRepository AddRange(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().AddRange(entities);
            return this;
        }

        public IRepository Update(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
            return this;
        }

        public IRepository Update(TEntity entity, params Expression<Func<TEntity, object>>[] properties)
        {
            var entry = Context.Entry(entity);
            entry.State = EntityState.Modified;

            foreach (var property in properties)
                entry.Property(property).IsModified = false;

            return this;
        }

        public IRepository Set(TEntity entity, params Expression<Func<TEntity, object>>[] properties)
        {
            var entry = Context.Entry(entity);
            Context.Set<TEntity>().Attach(entity);

            foreach(var property in properties)
                entry.Property(property).IsModified = true;

            return this;
        }

        public IRepository Remove(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
            return this;
        }

        public IRepository RemoveRange(IEnumerable<TEntity> entities)
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
        IRepository Add(TEntity entity);
        IRepository AddRange(IEnumerable<TEntity> entities);
        IRepository Update(TEntity entity);
        IRepository Update(TEntity entity, params Expression<Func<TEntity, object>>[] properties);
        IRepository Set(TEntity entity, params Expression<Func<TEntity, object>>[] properties);
        IRepository Remove(TEntity entity);
        IRepository RemoveRange(IEnumerable<TEntity> entities);
        TEntity Get(int id);
        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate);
        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate);
    }

    public interface IRepository
    {
        void Save();
    }
}
