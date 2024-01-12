using System.Security.Claims;

using TTP.Domain.Entities;

namespace TTP.Application.Services.Interfaces;

public interface IJwtService
{
    string GenerateToken(User user, string[] tenants, IList<Claim>? customClaims = null);
    bool ValidateToken(string token);
    ClaimsPrincipal GetPrincipalFromToken(string token);
}
