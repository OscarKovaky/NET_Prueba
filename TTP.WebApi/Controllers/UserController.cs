using Microsoft.AspNetCore.Mvc;
using TTP.Application.Services.Interfaces;
using TTP.Contract.Organization;

namespace TTP.WebApi.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IUserIdentityService _userIdentityService;

    public UserController(IUserService userService, IUserIdentityService userIdentityService)
    {
        _userService = userService;
        _userIdentityService = userIdentityService;
    }

    [HttpPost]
    [Route("organization/associate")]
    public async Task<bool> Post(AssociateRequest request)
    {
        var userId = _userIdentityService.UserId();
        return await _userService.AssociateTenant(userId, request.TenantSlug);
    }
}