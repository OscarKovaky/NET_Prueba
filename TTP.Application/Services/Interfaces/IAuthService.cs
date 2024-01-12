using TTP.Contract.Auth;

namespace TTP.Application.Services.Interfaces;

public interface IAuthService
{
    Task<LoginResponse> Login(LoginRequest request);
}