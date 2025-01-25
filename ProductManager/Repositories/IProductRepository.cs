using System;
using ProductManager.Entities;

namespace ProductManager.Repositories;

public interface IProductRepository
{
    public Task<List<Product>> GetPaginatedProducts(int pageIndex, int pageSize);
    public Task<List<Product>> GetAllProducts();
    public Task<int> GetTotalCount();
    public Task<Product?> GetProduct(Guid id);
    public void UpdateDescription(Guid id, string description);
}
