namespace TTP.Application.Services.Interfaces;

public interface IUserIdentityService
{
    long UserId();
    string[] Tenants();
}