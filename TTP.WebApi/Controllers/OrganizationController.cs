using Microsoft.AspNetCore.Mvc;
using TTP.Application.Services.Interfaces;
using TTP.Contract.Organization;
using TTP.Domain.Entities;

namespace TTP.WebApi.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class OrganizationController : ControllerBase
{
    private readonly IUserIdentityService _userIdentityService;
    private readonly IOrganizationService _organizationService;

    public OrganizationController(IUserIdentityService userIdentityService, IOrganizationService organizationService)
    {
        _userIdentityService = userIdentityService;
        _organizationService = organizationService;
    }

    [HttpGet]
    [Route("user")]
    public async Task<IEnumerable<Organization>> Get()
    {
        var userId = _userIdentityService.UserId();
        var organizations = await _organizationService.GetOrganizations(userId);

        return organizations;
    }

    [HttpPost]
    public async Task<long> Post(OrganizationRequest request)
    {
        var result = await _organizationService.Create(request);
        return result;
    }
}