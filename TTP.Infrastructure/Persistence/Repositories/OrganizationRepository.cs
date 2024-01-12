using TTP.Application.Persistence.Repositories;
using TTP.Domain.Entities;

namespace TTP.Infrastructure.Persistence.Repositories;

public class OrganizationRepository : SecurityRepository<Organization>, IOrganizationRepository
{
    public OrganizationRepository(SecurityDbContext securityDbContext) : base(securityDbContext)
    {
    }
}