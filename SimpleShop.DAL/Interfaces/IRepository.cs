using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SimpleShop.DAL.Interfaces;

public interface IRepository<TEntity> where TEntity : class
{
    IEnumerable<TEntity> GetAll();
    TEntity GetById(int id);

    IEnumerable<TEntity> Get(
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        string includeProperties = "");

    void Add(TEntity entity);
    void Update(TEntity item);
    void Delete(TEntity item);
    void AddRange(IEnumerable<TEntity> entity);
    IQueryable<TEntity> All(string includeProperties = "");
}