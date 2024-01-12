namespace TTP.Application.Services.Interfaces;

public interface IUserService
{
    Task<bool> AssociateTenant(long userId, string slugTenant);
    Task VerifyUserOrganization(long userId, string slug);
}