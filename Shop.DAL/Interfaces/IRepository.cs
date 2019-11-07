using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Shop.DAL.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
        TEntity GetById(int id);
        IEnumerable<TEntity> Get(
        Expression<Func<TEntity, bool>> filter = null,
        Func<IQueryable<TEntity>,
        IOrderedQueryable<TEntity>> orderBy = null,
        string includeProperties = "");
        TEntity Add(TEntity entity);
        void Update(TEntity item);
        void Delete(TEntity item);
        void SetStateModified(TEntity entity);
        IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entity);
        IQueryable<TEntity> All(string includeProperties = "");
        IQueryable<TEntity> GetQ(string includeProperties = "");
    }
}
