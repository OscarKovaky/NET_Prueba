using Microsoft.Extensions.Configuration;
using TTP.Application.Persistence.Repositories;
using TTP.Domain.Entities;

namespace TTP.Infrastructure.Persistence.Repositories;

public class ProductRepository : ApplicationRepository<Product>, IProductRepository
{
    
    public ProductRepository(ApplicationDbContext applicationDbContext, IConfiguration configuration) : 
        base(applicationDbContext, configuration)
    {

    }
}