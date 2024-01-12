
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

using Microsoft.Extensions.Options;

using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

using TTP.Application.Services.Interfaces;
using TTP.Domain.Entities;
using TTP.Infrastructure.Authentication;

namespace TTP.Infrastructure.Services;

public class JwtService : IJwtService
{
    private readonly JwtSettings _options;

    public JwtService(IOptions<JwtSettings> options)
    {
        _options = options.Value;
    }

    public string GenerateToken(User user, string[] tenants, IList<Claim>? customClaims = null)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Locality, string.Join(';', tenants)),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        if (customClaims != null)
        {
            claims.AddRange(customClaims);
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Secret));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _options.Issuer,
            audience: _options.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(_options.ExpiryHours),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public bool ValidateToken(string token)
    {
        var isValid = false;

        try
        {
            var claims = GetPrincipalFromToken(token);
            isValid = true;
        }
        catch 
        {
            isValid = false;
        }

        return isValid;
    }

    public ClaimsPrincipal GetPrincipalFromToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Secret));
        SecurityToken outToken = new JwtSecurityToken();
        var parameters = new TokenValidationParameters()
        {
            IssuerSigningKey = key,
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidIssuer = _options.Issuer,
            ValidAudience = _options.Audience,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
        var claims = tokenHandler.ValidateToken(token, parameters, out outToken);
        return claims;
    }
}