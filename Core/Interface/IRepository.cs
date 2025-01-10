using System;
using System.Linq.Expressions;

namespace Core.Interface;

public interface IRepository<T> where T :class
{
    Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>>? filter = null, string? includeProperties = null);
    Task<T> GetByIdAsync(Expression<Func<T, bool>>? filter, string? includeProperties = null);
    void Add(T entity);
    void Delete(T entity);
    void Update(T entity);

}
