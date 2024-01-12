using TTP.Contract.Auth;
using TTP.Domain.Entities;
using TTP.Application.Services.Interfaces;
using TTP.Application.Persistence.Repositories;
using TTP.Domain.Exceptions;

namespace TTP.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtService _jwtService;

    public AuthService(IUserRepository userRepository, IJwtService jwtService)
    {
        _userRepository = userRepository;
        _jwtService = jwtService;
    }

    public async Task<LoginResponse> Login(LoginRequest request)
    {
        var user = await _userRepository.GetAsync(user => user.Email == request.Email);
    
        if (user == null)
            throw new NotFoundException($"{nameof(user)} not found");

        if(!Common.Helpers.PasswordHashHelper.VerifyPassword(user.Password, request.Password))
            throw new NotFoundException($"{nameof(user)} Password Incorrect!");

        var organizations = await _userRepository.GetOrganizations(user.Id);
        var tenants = organizations.Select(org => new Slug { SlugTenant = org.SlugTenant }).ToArray();
        var response = new LoginResponse
        {
            AccessToken = _jwtService.GenerateToken(user, tenants.Select(tenant => tenant.SlugTenant).ToArray()),
            Tenants = tenants
        };
        return response;
    }
}