using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TTP.Domain.Entities;
using TTP.Infrastructure.Migrations.Seeds;

namespace TTP.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    private readonly IConfiguration _configuration;
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration) : base(options)
    {
        _configuration = configuration;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ProductSeeds();
    }

    public void ChangeDatabase(string tenant)
    {
        _configuration["ConnectionStrings:Application"] = $"Data Source=./DataNirvana/{tenant}.db";
    }
    
    public async Task CreateDatabase()
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        var connectionString = _configuration.GetConnectionString("Application");
        optionsBuilder.UseSqlite(connectionString);
        
        var dbContext = new ApplicationDbContext(optionsBuilder.Options, _configuration);
        await dbContext.Database.MigrateAsync();
    }
    
    private DbSet<Product> Products { get; set; } = default!;
}