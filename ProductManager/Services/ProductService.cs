using System;
using System.Threading.Tasks;
using AutoMapper;
using ProductManager.Entities;
using ProductManager.Exceptions;
using ProductManager.Models.Dto;
using ProductManager.Repositories;

namespace ProductManager.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    public ProductService(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;    
        _mapper = mapper;
    }

    public async Task<List<ProductDto>> GetPaginatedProducts(int pageIndex, int pageSize)
    {
        List<Product> products = await _productRepository.GetPaginatedProducts(pageIndex, pageSize);
        List<ProductDto> dtoProducts = _mapper.Map<List<ProductDto>>(products);

        return dtoProducts;
    }

    public async Task<List<ProductDto>> GetAllProducts()
    {
        List<Product> products = await _productRepository.GetAllProducts();
        List<ProductDto> dtoProducts = _mapper.Map<List<ProductDto>>(products);

        return dtoProducts;
    }

    public async Task<int> GetTotalPagesCount(int pageSize)
    {
        var count = await _productRepository.GetTotalCount();
        var totalPages = (int)Math.Ceiling(count / (double)pageSize);

        return totalPages;
    }

    public async Task<ProductDto> GetProduct(Guid id)
    {
        Product? product = await _productRepository.GetProduct(id);
        
        if (product == null)
            throw new NotFoundException($"Product with ID {id} not found.");

        ProductDto dtoProduct = _mapper.Map<ProductDto>(product);

        return dtoProduct;
    }

    public async Task UpdateDescription(Guid id, string description)
    {
        await _productRepository.UpdateDescription(id, description);
    }
}
