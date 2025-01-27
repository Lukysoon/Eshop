using Microsoft.EntityFrameworkCore;
using ProductManager.Data;
using ProductManager.Entities;
using ProductManager.Repositories;
using ProductManager.Tests.Data;

namespace ProductManager.Tests.Repositories;

public class ProductRepositoryTests : IDisposable
{
    private readonly ApplicationDbContext _context;
    private readonly ProductRepository _repository;
    private readonly List<Product> _testProducts;

    public ProductRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new ApplicationDbContext(options);
        _repository = new ProductRepository(_context);

        // Setup test data
        _testProducts = ProductData.GetProducts();

        _context.Products.AddRange(_testProducts);
        _context.SaveChanges();
    }

    [Fact]
    public async Task GetAllProducts_ShouldReturnAllProducts()
    {
        // Act
        var result = await _repository.GetAllProducts();

        // Assert
        Assert.Equal(_testProducts.Count, result.Count);
        Assert.Equal(_testProducts[0].Id, result[0].Id);
        Assert.Equal(_testProducts[1].Id, result[1].Id);
        Assert.Equal(_testProducts[2].Id, result[2].Id);
    }

    [Fact]
    public async Task GetPaginatedProducts_ShouldReturnCorrectPage()
    {
        int pageIndex = 1;
        int pageSize = 2;

        var result = await _repository.GetPaginatedProducts(pageIndex, pageSize);

        Assert.Equal(2, result.Count);
        Assert.Contains(_testProducts[0].Id, result.Select(p => p.Id));
        Assert.Contains(_testProducts[1].Id, result.Select(p => p.Id));
    }

    [Fact]
    public async Task GetProduct_ShouldReturnCorrectProduct()
    {
        var expectedId = _testProducts[1].Id;

        var result = await _repository.GetProduct(expectedId);

        Assert.NotNull(result);
        Assert.Equal(expectedId, result.Id);
        Assert.Equal(_testProducts[1].Name, result.Name);
        Assert.Equal(_testProducts[1].Description, result.Description);
    }

    [Fact]
    public async Task GetProduct_WithInvalidId_ShouldReturnNull()
    {
        var invalidId = Guid.NewGuid();

        var result = await _repository.GetProduct(invalidId);

        Assert.Null(result);
    }

    [Fact]
    public async Task GetTotalCount_ShouldReturnCorrectCount()
    {
        var result = await _repository.GetTotalCount();

        Assert.Equal(_testProducts.Count, result);
    }

    [Fact]
    public async Task UpdateDescription_ShouldUpdateProductDescription()
    {
        var productId = _testProducts[0].Id;
        var newDescription = "Updated Description";

        await _repository.UpdateDescription(productId, newDescription);
        var updatedProduct = await _repository.GetProduct(productId);

        Assert.NotNull(updatedProduct);
        Assert.Equal(newDescription, updatedProduct.Description);
    }

    public void Dispose()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }
}
