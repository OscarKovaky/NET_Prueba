namespace TTP.Domain.Entities;

public class User : Base.EntityBase
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public ICollection<UserOrganization> UserOrganizations { get; set; } = new List<UserOrganization>();
}