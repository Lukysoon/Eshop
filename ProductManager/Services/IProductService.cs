using System;
using ProductManager.Models.Dto;

namespace ProductManager.Services;

public interface IProductService
{
    public Task<List<ProductDto>> GetProducts(int pageIndex, int pageSize);
    public Task<int> GetTotalPagesCount(int pageIndex, int pageSize);
}
