namespace TTP.Domain.Entities;

public class Organization : Base.EntityBase
{
    public string Name { get; set; } = string.Empty;
    public string SlugTenant { get; set; } = string.Empty;
}