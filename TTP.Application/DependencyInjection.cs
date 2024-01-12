
using Microsoft.Extensions.DependencyInjection;
using TTP.Application.Services;
using TTP.Application.Services.Interfaces;

namespace TTP.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IOrganizationService, OrganizationService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IProductService, ProductService>();
        
        return services;
    }
}