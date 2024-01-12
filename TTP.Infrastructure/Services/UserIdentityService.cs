using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using TTP.Application.Services.Interfaces;

namespace TTP.Infrastructure.Services
{
    public class UserIdentityService : IUserIdentityService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserIdentityService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public long UserId()
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim != null && long.TryParse(userIdClaim.Value, out var userId))
            {
                return userId;
            }

            throw new InvalidOperationException("No se pudo encontrar el Claim 'NameIdentifier' en el contexto del usuario.");
        }

        public string[] Tenants()
        {
            var localityClaim = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Locality);
            if (localityClaim != null)
            {
                return localityClaim.Value.Split(';');
            }

            throw new InvalidOperationException("No se pudo encontrar el Claim 'Locality' en el contexto del usuario.");
        }
    }
}