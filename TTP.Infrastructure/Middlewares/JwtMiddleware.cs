using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using TTP.Application.Services.Interfaces;

namespace TTP.Infrastructure.Middlewares
{
    public class JwtMiddleware : IMiddleware
    {
        private readonly IJwtService _jwtService;

        public JwtMiddleware(IJwtService jwtService)
        {
            _jwtService = jwtService;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var endpoint = context.GetEndpoint();
            if (endpoint?.Metadata?.GetMetadata<IAllowAnonymous>() != null)
            {
                await next(context);
                return; // Permitir acceso anónimo
            }

            if (context.Request.Headers.TryGetValue("Authorization", out var tokenValue))
            {
                var token = tokenValue.ToString().Replace("Bearer ", "");

                if (_jwtService.ValidateToken(token))
                {
                    context.User = _jwtService.GetPrincipalFromToken(token);
                    await next(context);
                    return;
                }
            }

            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        }
    }
}