using System.ComponentModel.DataAnnotations.Schema;

namespace TTP.Domain.Entities;

public sealed class UserOrganization : Base.EntityBase
{
    public long UserId { get; set; }
    public long OrganizationId { get; set; }
    
    [ForeignKey("UserId")]
    public User User { get; set; } = null!;

    [ForeignKey("OrganizationId")]
    public Organization Organization { get; set; } = null!;
}