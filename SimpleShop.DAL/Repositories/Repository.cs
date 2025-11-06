using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Shop.DAL.EF;
using Shop.DAL.Interfaces;

namespace Shop.DAL.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    protected readonly ApplicationDbContext Context;
    private readonly DbSet<TEntity> _dbSet;

    protected Repository(ApplicationDbContext mainDbContext)
    {
        Context = mainDbContext;
        _dbSet = Context.Set<TEntity>();
    }

    public virtual IQueryable<TEntity> All(string includeProperties = "")
    {
        return includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
            .Aggregate<string?, IQueryable<TEntity>>(_dbSet,
                (current, includeProperty) => current.Include(includeProperty));
    }

    public virtual IEnumerable<TEntity> GetAll()
    {
        return _dbSet.ToList();
    }

    public virtual TEntity GetById(int id)
    {
        return _dbSet.Find(id);
    }

    public virtual IEnumerable<TEntity> Get(
        Expression<Func<TEntity, bool>>? filter = null, 
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, 
        string includeProperties = "")
    {
        IQueryable<TEntity> query = _dbSet;

        if (filter != null) query = query.Where(filter);

        query = includeProperties.Split([','], StringSplitOptions.RemoveEmptyEntries)
            .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

        return orderBy != null ? orderBy(query).ToList() : query.ToList();
    }

    public virtual void Add(TEntity entity)
    {
        if (entity == null) throw new NotImplementedException();
        _dbSet.Add(entity);
    }

    public virtual void AddRange(IEnumerable<TEntity> entity)
    {
        if (entity == null) throw new NotImplementedException();
        _dbSet.AddRange(entity);
    }

    public virtual void Update(TEntity item)
    {
        try
        {
            _dbSet.Attach(item);
        }
        finally
        {
            _dbSet.Update(item);
        }
    }

    public void Delete(TEntity item)
    {
        if (item == null) throw new NotImplementedException();
        _dbSet.Remove(item);
    }

    public void SetStateModified(TEntity entity)
    {
        Context.Entry(entity).State = EntityState.Modified;
    }
}