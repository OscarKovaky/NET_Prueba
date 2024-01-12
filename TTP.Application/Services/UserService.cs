using TTP.Application.Persistence.Repositories;
using TTP.Application.Services.Interfaces;
using TTP.Domain.Entities;

namespace TTP.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IOrganizationRepository _organizationRepository;
    private readonly IRepository<UserOrganization> _userOrganizationRepository;

    public UserService(IUserRepository userRepository, IOrganizationRepository organizationRepository, IRepository<UserOrganization> userOrganizationRepository)
    {
        _userRepository = userRepository;
        _organizationRepository = organizationRepository;
        _userOrganizationRepository = userOrganizationRepository;
    }

    public async Task<bool> AssociateTenant(long userId, string slugTenant)
    {
        var organization = await _organizationRepository.GetAsync(org => org.SlugTenant == slugTenant);
        if (organization == null)
            throw new Exception($"Organization with slug '{slugTenant}' not exists.");
        
        var result = await _userRepository.AssociateTenant(userId, organization.Id);
        return result;
    }

    public async Task VerifyUserOrganization(long userId, string slug)
    {
        var organization = await _organizationRepository.GetAsync(org => org.SlugTenant == slug);
        if (organization is null)
            throw new Exception("The Organization dont exists.");

        var relation = await _userOrganizationRepository.GetAsync(rel => rel.UserId == userId && rel.OrganizationId == organization.Id);
        if (relation is null)
            throw new Exception("The user dont have a Organization Associated.");
    }
}