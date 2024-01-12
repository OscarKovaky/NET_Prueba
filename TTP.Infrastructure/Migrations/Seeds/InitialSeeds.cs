using Microsoft.EntityFrameworkCore;
using TTP.Domain.Entities;

namespace TTP.Infrastructure.Migrations.Seeds;

public static class Seeds
{
    public static void InitialSeeds(this ModelBuilder modelBuilder)
    {
        var password = Common.Helpers.PasswordHashHelper.HashPassword("some-password");
        var userList = new List<User>
        {
            new User { Id = 1, Email = "pucheta.adrian@gmail.com", Password = password },
            new User { Id = 2, Email = "zephirotube@gmail.com", Password = password }
        };

        modelBuilder.Entity<User>().HasData(userList);
    }

    public static void ProductSeeds(this ModelBuilder modelBuilder)
    {
        var products = new List<Product>
        {
            new Product { Id = 1, Name = "Lambda Functions", Price = 0.01 },
            new Product { Id = 2, Name = "SQS Messages", Price = 0.05 },
            new Product { Id = 3, Name = "Databases SQL", Price = 0.48 },
            new Product { Id = 4, Name = "Databases NoSQL", Price = 0.38 },
            new Product { Id = 5, Name = "Elastic Cache", Price = 0.11 },
            new Product { Id = 6, Name = "Cluster Docker Containers", Price = 1.37 },
        };

        modelBuilder.Entity<Product>().HasData(products);
    }
}