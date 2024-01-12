using TTP.Contract.Organization;
using TTP.Domain.Entities;

namespace TTP.Application.Services.Interfaces;

public interface IOrganizationService
{
    Task<IEnumerable<Organization>> GetOrganizations(long userId);
    Task<long> Create(OrganizationRequest request);
}