using System.Linq.Expressions;
using TTP.Domain.Entities.Base;

namespace TTP.Application.Persistence.Repositories;

public interface IRepository<T> where T : EntityBase
{
    void ChangeConnectionString();
    Task<T?> GetAsync(long id);
    Task<T?> GetAsync(Expression<Func<T, bool>> predicate);
    Task<IEnumerable<T>> GetAllAsync();
    Task<long> AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
    Task<IEnumerable<T>> GetManyAsync(Expression<Func<T, bool>> predicate);
    Task<IEnumerable<T>> GetManyAsync(Expression<Func<T, bool>> predicate, int? skip, int? take);
}