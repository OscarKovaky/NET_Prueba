namespace TTP.Application.Services.Interfaces;

public interface IDatabaseService
{
    Task<bool> NewDatabase(string slug);
    Task<bool> ChangeTenant(string slugTenant);
}