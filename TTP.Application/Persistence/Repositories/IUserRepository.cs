using TTP.Contract.Auth;
using TTP.Domain.Entities;

namespace TTP.Application.Persistence.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<IEnumerable<Organization>> GetOrganizations(long userId);
    Task<bool> AssociateTenant(long userId, long organizationId);
}