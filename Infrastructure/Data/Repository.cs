using System;
using System.Linq.Expressions;
using Core.Interface;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly ApplicationContext _context;
    internal DbSet<T> _dbSet;
    public Repository(ApplicationContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public void Add(T entity)
    {
        _dbSet.Add(entity);
    }

    public void Delete(T entity)
    {
        _dbSet.Remove(entity);
    }

    public async Task<T> GetByIdAsync(Expression<Func<T, bool>>? filter, string? includeProperties = null)
    {
        IQueryable<T> query = _dbSet;
        if(filter != null)
        {
            query = query.Where(filter);
        }
        if(!string.IsNullOrEmpty(includeProperties))
        {
            foreach(var includeProp in includeProperties.Split(new char[] {','},StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProp);   
            }
        }

        return await query.FirstOrDefaultAsync();
    }

    public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>>? filter = null, string? includeProperties = null)
    {
        IQueryable<T> query = _dbSet;
        if(filter !=null)
        {
            query = query.Where(filter);
        }
        if(!string.IsNullOrEmpty(includeProperties))
        {
            foreach(var includeProp in includeProperties.Split(new char[] {','},StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProp);   
            }
        }

        return await query.ToListAsync();
    }

    public void Update(T entity)
    {
        _dbSet.Entry(entity).State = EntityState.Modified;
    }
}
