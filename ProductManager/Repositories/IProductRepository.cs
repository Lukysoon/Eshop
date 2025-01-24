using System;
using ProductManager.Entities;

namespace ProductManager.Repositories;

public interface IProductRepository
{
    public Task<List<Product>> GetProducts(int pageIndex, int pageSize);
    public Task<int> GetTotalCount(int pageIndex, int pageSize);
}
