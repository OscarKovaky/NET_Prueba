using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

using Microsoft.Extensions.Configuration;
using TTP.Application.Persistence.Repositories;
using TTP.Domain.Entities.Base;
using TTP.Domain.Exceptions;

namespace TTP.Infrastructure.Persistence.Repositories;

public class ApplicationRepository<T> : IRepository<T> where T : EntityBase 
{
    private readonly ApplicationDbContext _applicationDbContext;
    private readonly IConfiguration _configuration;

    public ApplicationRepository(ApplicationDbContext applicationDbContext, IConfiguration configuration)
    {
        _applicationDbContext = applicationDbContext;
        _configuration = configuration;
    }

    public void ChangeConnectionString()
    {
        var strConn = _configuration.GetConnectionString("Application");
        _applicationDbContext.Database.SetConnectionString(strConn);
    }

    public virtual async Task<T?> GetAsync(long id)
    {
        return await _applicationDbContext.Set<T>().FindAsync(id);
    }

    public virtual async Task<T?> GetAsync(Expression<Func<T, bool>> predicate)
    {
        return await _applicationDbContext.Set<T>()
            .AsNoTracking()
            .FirstOrDefaultAsync(predicate);
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _applicationDbContext.Set<T>().AsNoTracking().ToListAsync();
    }

    public virtual async Task<IEnumerable<T>> GetManyAsync(Expression<Func<T, bool>> predicate)
    {
        return await _applicationDbContext.Set<T>()
            .AsNoTracking()
            .Where(predicate)
            .ToListAsync();
    }

    public virtual async Task<IEnumerable<T>> GetManyAsync(Expression<Func<T, bool>> predicate, int? skip, int? take)
    {
        var queryable = _applicationDbContext.Set<T>()
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
        var result = await _applicationDbContext.Set<T>().AddAsync(entity);
        await _applicationDbContext.SaveChangesAsync();

        return result.Entity.Id;
    }

    public virtual async Task UpdateAsync(T entity)
    {
        var retrievedEntity = await _applicationDbContext.Set<T>().FindAsync(entity.Id);
        if(retrievedEntity is null)
            throw new NotFoundException($"Can't update entity {typeof(T).Name} not found entity with id: {entity.Id}");

        _applicationDbContext.Entry(retrievedEntity).CurrentValues.SetValues(entity);
        await _applicationDbContext.SaveChangesAsync();
    }

    public virtual async Task DeleteAsync(T entity)
    {
        _applicationDbContext.Set<T>().Remove(entity);
        await _applicationDbContext.SaveChangesAsync();
    }
}