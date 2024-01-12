using TTP.Application.Services.Interfaces;
using TTP.Infrastructure.Persistence;

namespace TTP.Infrastructure.Services;

public class DatabaseService : IDatabaseService
{
    private readonly ApplicationDbContext _applicationDbContext;

    public DatabaseService(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<bool> NewDatabase(string slug)
    {
        _applicationDbContext.ChangeDatabase(slug);
        await _applicationDbContext.CreateDatabase();
        return true;
    }

    public Task<bool> ChangeTenant(string slugTenant)
    {
        _applicationDbContext.ChangeDatabase(slugTenant);
        return Task.FromResult(true);
    }
}