using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using TTP.Contract.Auth;
using TTP.Application.Services.Interfaces;

namespace TTP.WebApi.Controllers;

[ApiController]
[AllowAnonymous]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }
    
    [HttpPost(Name = "Login")]
    public async Task<LoginResponse> Post(LoginRequest request)
    {
        var response = await _authService.Login(request);
        return response;
    }
}