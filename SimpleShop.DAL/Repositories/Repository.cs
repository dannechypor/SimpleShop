using Shop.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Shop.DAL.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly ApplicationDbContext context;
        protected readonly DbSet<TEntity> dbSet;

        public Repository(ApplicationDbContext mainDbContext)
        {
            context = mainDbContext;
            dbSet = context.Set<TEntity>();
        }
        public virtual IQueryable<TEntity> All(string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;
            foreach (var includeProperty in
                includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return dbSet.ToList();
        }

        public virtual TEntity GetById(int id)
        {
            return dbSet.Find(id);
        }

        public virtual List<TEntity> GetRangeById(List<TEntity> entities)
        {
            dbSet.Find(entities);
            return entities;
        }

        public IQueryable<TEntity> GetQ(string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;
            foreach (var includeProperty in
                includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>,
            IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public virtual TEntity Add(TEntity entity)
        {
            if (entity == null)
            {
                throw new NotImplementedException();
            }
            dbSet.Add(entity);
            return entity;

        }

        public virtual IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entity)
        {
            if (entity == null)
            {
                throw new NotImplementedException();
            }
            dbSet.AddRange(entity);
            return entity;

        }

        public virtual void Update(TEntity item)
        {
            try
            {
                dbSet.Attach(item);
            }
            catch { }
            finally
            {
                dbSet.Update(item);
            }
        }

        public void Delete(TEntity item)
        {
            if (item == null)
            {
                throw new NotImplementedException();
            }
            dbSet.Remove(item);
        }

        public void SetStateModified(TEntity entity)
        {
            context.Entry<TEntity>(entity).State = EntityState.Modified;
        }
    }
}
