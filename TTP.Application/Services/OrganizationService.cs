using TTP.Application.Persistence.Repositories;
using TTP.Application.Services.Interfaces;
using TTP.Contract.Organization;
using TTP.Domain.Entities;

namespace TTP.Application.Services;

public class OrganizationService : IOrganizationService
{
    private readonly IDatabaseService _databaseService;
    private readonly IUserRepository _userRepository;
    private readonly IOrganizationRepository _organizationRepository;

    public OrganizationService(IDatabaseService databaseService, IUserRepository userRepository, IOrganizationRepository organizationRepository)
    {
        _databaseService = databaseService;
        _userRepository = userRepository;
        _organizationRepository = organizationRepository;
    }

    public async Task<IEnumerable<Organization>> GetOrganizations(long userId)
    {
        var organizations = await _userRepository.GetOrganizations(userId);
        return organizations;
    }

    public async Task<long> Create(OrganizationRequest request)
    {
        var exists = await _organizationRepository.GetAsync(org => org.SlugTenant == request.Slug);

        if (exists != null)
        {
            throw new Exception($"Organization '{request.Name}' Exists");
        }

        var result = await _organizationRepository.AddAsync(new Organization()
            { Name = request.Name, SlugTenant = request.Slug });

        var isCreated = await _databaseService.NewDatabase(request.Slug);

        if (!isCreated)
            throw new Exception($"Database for '{request.Name}' have error on created.");

        return result;
    }
}