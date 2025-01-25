using System;
using ProductManager.Models.Dto;

namespace ProductManager.Services;

public interface IProductService
{
    public Task<List<ProductDto>> GetPaginatedProducts(int pageIndex, int pageSize);
    public Task<List<ProductDto>> GetAllProducts();
    public Task<int> GetTotalPagesCount(int pageIndex, int pageSize);
    public Task<ProductDto> GetProduct(Guid id);
    public Task UpdateDescription(Guid id, string description);
}
