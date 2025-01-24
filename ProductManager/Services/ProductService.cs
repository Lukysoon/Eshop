using System;
using ProductManager.Entities;
using ProductManager.Models.Dto;
using ProductManager.Repositories;

namespace ProductManager.Services;

public class ProductService : IProductService
{
    readonly IProductRepository _productRepository;
    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;    
    }

    public async Task<List<ProductDto>> GetPaginatedProducts(int pageIndex, int pageSize)
    {
        // List<Product> products = await _productRepository.GetProducts(pageIndex, pageSize);
        throw new NotImplementedException();
    }

    public async Task<List<ProductDto>> GetAllProducts()
    {
        // List<Product> products = await _productRepository.GetAllProducts();
        throw new NotImplementedException();
    }

    public async Task<int> GetTotalPagesCount(int pageIndex, int pageSize)
    {
        var count = await _productRepository.GetTotalCount(pageIndex, pageSize);
        var totalPages = (int)Math.Ceiling(count / (double)pageSize);

        return totalPages;
    }

    public Task<ProductDto> GetProduct(Guid id)
    {
        // Product product = _productRepository.GetProduct(id);
        throw new NotImplementedException();
    }
    
}
