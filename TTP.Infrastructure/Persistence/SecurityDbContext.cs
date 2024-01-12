using Microsoft.EntityFrameworkCore;
using TTP.Domain.Entities;
using TTP.Infrastructure.Migrations.Seeds;

namespace TTP.Infrastructure.Persistence;

public class SecurityDbContext : DbContext
{
    public SecurityDbContext(DbContextOptions<SecurityDbContext> options) : base(options) { }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.InitialSeeds();
    }
    
    public DbSet<User> Users { get; set; } = default!;
    public DbSet<Organization> Organizations { get; set; } = default!;
    public DbSet<UserOrganization> UserOrganizations { get; set; } = default!;
}