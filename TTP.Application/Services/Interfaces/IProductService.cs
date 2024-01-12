using TTP.Contract;
using TTP.Domain.Entities;

namespace TTP.Application.Services.Interfaces;

public interface IProductService
{
    Task<IEnumerable<Product>> GetProducts(long userId, string slugTenant);
    Task<long> SaveProducts(long userId, string slugTenant, ProductRequest request);
}