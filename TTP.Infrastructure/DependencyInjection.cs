using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using TTP.Application.Persistence.Repositories;
using TTP.Application.Services;
using TTP.Application.Services.Interfaces;
using TTP.Infrastructure.Authentication;
using TTP.Infrastructure.Middlewares;
using TTP.Infrastructure.Persistence;
using TTP.Infrastructure.Persistence.Repositories;
using TTP.Infrastructure.Services;

namespace TTP.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<SecurityDbContext>(options =>
        {
            options.UseSqlite(configuration.GetConnectionString("Security"));
        });
        
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            var strConn = configuration.GetConnectionString("Application");
            options.UseSqlite(strConn);
        });
        
        services.AddScoped(typeof(IRepository<>), typeof(ApplicationRepository<>));
        services.AddScoped(typeof(IRepository<>), typeof(SecurityRepository<>));
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IOrganizationRepository, OrganizationRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        
        services.AddScoped<IDatabaseService, DatabaseService>();
        
        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
        services.AddSingleton<IJwtService, JwtService>();
        services.AddTransient<JwtMiddleware>();
        services.AddTransient<TenantMiddleware>();
        services.AddScoped<IUserIdentityService, UserIdentityService>();
        services.AddScoped<IOrganizationService, OrganizationService>();

        return services;
    }
}