using System;
using ProductManager.Entities;

namespace ProductManager.Repositories;

public interface IProductRepository
{
    public Task<List<Product>> GetPaginatedProducts(int pageIndex, int pageSize);
    public Task<List<Product>> GetAllProducts();
    public Task<int> GetTotalCount(int pageIndex, int pageSize);
    Product GetProduct(Guid id);
}
