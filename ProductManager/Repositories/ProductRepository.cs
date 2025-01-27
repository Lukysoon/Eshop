using System;
using Microsoft.EntityFrameworkCore;
using ProductManager.Data;
using ProductManager.Entities;
using ProductManager.Exceptions;

namespace ProductManager.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly IApplicationDbContext _context;
    public ProductRepository(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Product>> GetAllProducts()
    {
        return await _context.Products.ToListAsync();
    }

    public async Task<List<Product>> GetPaginatedProducts(int pageIndex, int pageSize)
    {
        var products = await _context.Products
            .OrderBy(b => b.Id)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return products;
    }

    public async Task<Product?> GetProduct(Guid id)
    {
        return await _context.Products.Where(p => p.Id == id).FirstOrDefaultAsync();
    }

    public async Task<int> GetTotalCount()
    {
        return await _context.Products.CountAsync();
    }

    public async Task UpdateDescription(Guid id, string description)
    {
        var product = await _context.Products.Where(p => p.Id == id).FirstOrDefaultAsync();
        
        if (product == null)
            throw new NotFoundException($"Product with id {id} does not exist. Cannot update description.");

        product.Description = description;
        await _context.SaveChangesAsync();
    }
}
