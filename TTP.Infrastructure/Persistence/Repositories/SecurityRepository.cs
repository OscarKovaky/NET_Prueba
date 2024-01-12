using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TTP.Application.Persistence.Repositories;
using TTP.Domain.Entities.Base;
using TTP.Domain.Exceptions;

namespace TTP.Infrastructure.Persistence.Repositories;

public class SecurityRepository<T> : IRepository<T> where T : EntityBase 
{
    private readonly SecurityDbContext _securityDbContext;

    public SecurityRepository(SecurityDbContext securityDbContext)
    {
        _securityDbContext = securityDbContext;
    }

    public void ChangeConnectionString()
    {
        throw new NotImplementedException();
    }

    public virtual async Task<T?> GetAsync(long id)
    {
        return await _securityDbContext.Set<T>().FindAsync(id);
    }

    public virtual async Task<T?> GetAsync(Expression<Func<T, bool>> predicate)
    {
        return await _securityDbContext.Set<T>()
            .AsNoTracking()
            .FirstOrDefaultAsync(predicate);
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _securityDbContext.Set<T>().AsNoTracking().ToListAsync();
    }

    public virtual async Task<IEnumerable<T>> GetManyAsync(Expression<Func<T, bool>> predicate)
    {
        return await _securityDbContext.Set<T>()
            .AsNoTracking()
            .Where(predicate)
            .ToListAsync();
    }

    public virtual async Task<IEnumerable<T>> GetManyAsync(Expression<Func<T, bool>> predicate, int? skip, int? take)
    {
        var queryable = _securityDbContext.Set<T>()
            .AsNoTracking()
            .Where(predicate);

        if (skip is > 0)
            queryable = queryable.Skip(skip.Value);

        if (take is > 0)
            queryable = queryable.Take(take.Value);

        return await queryable.ToListAsync();
    }

    public virtual async Task<long> AddAsync(T entity)
    {
        var result = await _securityDbContext.Set<T>().AddAsync(entity);
        await _securityDbContext.SaveChangesAsync();

        return result.Entity.Id;
    }

    public virtual async Task UpdateAsync(T entity)
    {
        var retrievedEntity = await _securityDbContext.Set<T>().FindAsync(entity.Id);
        if(retrievedEntity is null)
            throw new NotFoundException($"Can't update entity {typeof(T).Name} not found entity with id: {entity.Id}");

        _securityDbContext.Entry(retrievedEntity).CurrentValues.SetValues(entity);
        await _securityDbContext.SaveChangesAsync();
    }

    public virtual async Task DeleteAsync(T entity)
    {
        _securityDbContext.Set<T>().Remove(entity);
        await _securityDbContext.SaveChangesAsync();
    }
}