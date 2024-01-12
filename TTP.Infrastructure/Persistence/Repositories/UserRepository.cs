using Microsoft.EntityFrameworkCore;
using TTP.Application.Persistence.Repositories;
using TTP.Contract.Auth;
using TTP.Domain.Entities;

namespace TTP.Infrastructure.Persistence.Repositories;

public class UserRepository : SecurityRepository<User>, IUserRepository
{
    private readonly SecurityDbContext _securityDbContext;
    private readonly IRepository<UserOrganization> _userOrganizationRepository;
    
    public UserRepository(
        SecurityDbContext securityDbContext, 
        IRepository<UserOrganization> userOrganizationRepository
    ) : base(securityDbContext)
    {
        _securityDbContext = securityDbContext;
        _userOrganizationRepository = userOrganizationRepository;
    }

    public async Task<IEnumerable<Organization>> GetOrganizations(long userId)
    {
        var query = $@"
            select 
                o.Id, o.Name, o.SlugTenant
            from UserOrganizations uo 
                inner join Organizations o on o.Id = uo.OrganizationId 
            where uo.UserId = {userId};";

        var organizations = await _securityDbContext.Organizations.FromSqlRaw(query).ToListAsync();
        return organizations;
    }

    public async Task<bool> AssociateTenant(long userId, long organizationId)
    {
        var entity = new UserOrganization()
        {
            UserId = userId,
            OrganizationId = organizationId
        };
        var id = await _userOrganizationRepository.AddAsync(entity);
        return id > 0;
    }
}