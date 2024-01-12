using Microsoft.AspNetCore.Mvc;
using TTP.Application.Services.Interfaces;
using TTP.Common;
using TTP.Contract;
using TTP.Domain.Entities;

namespace TTP.WebApi.Controllers;

[ApiController]
[Route("api/{tenant}/[Controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly IUserIdentityService _userIdentityService;

        
    public ProductController(IProductService productService, IUserIdentityService userIdentityService)
    {
        _productService = productService;
        _userIdentityService = userIdentityService;
    }
    
    [HttpGet]
    public async Task<IEnumerable<Product>> Get()
    {
        if (!HttpContext.Items.TryGetValue(Constants.Tenant, out var tenant))
        {
            return Enumerable.Empty<Product>();
        }

        var userId = _userIdentityService.UserId();
        return await _productService.GetProducts(userId, tenant!.ToString()!);
    }

    [HttpPost]
    public async Task<long> Post(ProductRequest request)
    {
        if (!HttpContext.Items.TryGetValue(Constants.Tenant, out var tenant))
        {
            return -1;
        }

        var userId = _userIdentityService.UserId();
        return await _productService.SaveProducts(userId, tenant!.ToString()!, request);
    }

}