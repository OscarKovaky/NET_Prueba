using TTP.Application.Persistence.Repositories;
using TTP.Application.Services.Interfaces;
using TTP.Contract;
using TTP.Domain.Entities;

namespace TTP.Application.Services;

public class ProductService : IProductService
{
    private readonly IDatabaseService _databaseService;
    private readonly IProductRepository _productRepository;
    private readonly IUserService _userService;

    public ProductService(IProductRepository productRepository, IDatabaseService databaseService, IUserService userService)
    {
        _productRepository = productRepository;
        _databaseService = databaseService;
        _userService = userService;
    }

    public async Task<IEnumerable<Product>> GetProducts(long userId, string slugTenant)
    {
        await _userService.VerifyUserOrganization(userId, slugTenant);
        await _databaseService.ChangeTenant(slugTenant);
        _productRepository.ChangeConnectionString();
        return await _productRepository.GetAllAsync();
    }

    public async Task<long> SaveProducts(long userId, string slugTenant, ProductRequest request)
    {
        await _userService.VerifyUserOrganization(userId, slugTenant);
        await _databaseService.ChangeTenant(slugTenant);
        _productRepository.ChangeConnectionString();

        var product = await _productRepository.GetAsync(prod => prod.Name == request.Name);
        if (product is not null)
            throw new Exception($"The Product '{request.Name}' al ready exists.");
        
        return await _productRepository.AddAsync(new Product()
        {
            Name = request.Name,
            Price = request.Price
        });

    }
}