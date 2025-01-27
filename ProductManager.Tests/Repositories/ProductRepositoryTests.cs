using Microsoft.EntityFrameworkCore;
using Moq;
using ProductManager.Data;
using ProductManager.Entities;
using ProductManager.Repositories;

namespace ProductManager.Tests.Repositories;

public class ProductRepositoryTests
{
    private readonly Mock<IApplicationDbContext> _contextMock;
    private readonly ProductRepository _repository;
    private readonly List<Product> _products;

    public ProductRepositoryTests()
    {
        _contextMock = new Mock<IApplicationDbContext>();
        _repository = new ProductRepository(_contextMock.Object);
        
        // Setup test data
        _products = new List<Product>
        {
            new Product { Id = Guid.NewGuid(), Name = "Product 1", Description = "Description 1", Price = 10.99m },
            new Product { Id = Guid.NewGuid(), Name = "Product 2", Description = "Description 2", Price = 20.99m },
            new Product { Id = Guid.NewGuid(), Name = "Product 3", Description = "Description 3", Price = 30.99m }
        };
    }

    [Fact]
    public async Task GetAllProducts_ShouldReturnAllProducts()
    {
        // Arrange
        var dbSetMock = new Mock<DbSet<Product>>();
        dbSetMock.As<IQueryable<Product>>().Setup(m => m.Provider).Returns(_products.AsQueryable().Provider);
        dbSetMock.As<IQueryable<Product>>().Setup(m => m.Expression).Returns(_products.AsQueryable().Expression);
        dbSetMock.As<IQueryable<Product>>().Setup(m => m.ElementType).Returns(_products.AsQueryable().ElementType);
        dbSetMock.As<IQueryable<Product>>().Setup(m => m.GetEnumerator()).Returns(_products.GetEnumerator());
        
        dbSetMock.As<IQueryable<Product>>().Setup(m => m.GetEnumerator()).Returns(_products.GetEnumerator());
        
        _contextMock.Setup(x => x.Products).Returns(dbSetMock.Object);

        // Act
        var result = await _repository.GetAllProducts();

        // Assert
        Assert.Equal(_products.Count, result.Count);
        Assert.Equal(_products, result);
    }

    [Fact]
    public async Task GetPaginatedProducts_ShouldReturnCorrectPage()
    {
        // Arrange
        var dbSetMock = new Mock<DbSet<Product>>();
        dbSetMock.As<IQueryable<Product>>().Setup(m => m.Provider).Returns(_products.AsQueryable().Provider);
        dbSetMock.As<IQueryable<Product>>().Setup(m => m.Expression).Returns(_products.AsQueryable().Expression);
        dbSetMock.As<IQueryable<Product>>().Setup(m => m.ElementType).Returns(_products.AsQueryable().ElementType);
        dbSetMock.As<IQueryable<Product>>().Setup(m => m.GetEnumerator()).Returns(_products.GetEnumerator());
        
        _contextMock.Setup(x => x.Products).Returns(dbSetMock.Object);

        // Act
        var result = await _repository.GetPaginatedProducts(1, 2);

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Equal(_products.Take(2).ToList(), result);
    }

    [Fact]
    public async Task GetProduct_ShouldReturnCorrectProduct()
    {
        // Arrange
        var targetProduct = _products[0];
        var dbSetMock = new Mock<DbSet<Product>>();
        dbSetMock.As<IQueryable<Product>>().Setup(m => m.Provider).Returns(_products.AsQueryable().Provider);
        dbSetMock.As<IQueryable<Product>>().Setup(m => m.Expression).Returns(_products.AsQueryable().Expression);
        dbSetMock.As<IQueryable<Product>>().Setup(m => m.ElementType).Returns(_products.AsQueryable().ElementType);
        dbSetMock.As<IQueryable<Product>>().Setup(m => m.GetEnumerator()).Returns(_products.GetEnumerator());
        
        _contextMock.Setup(x => x.Products).Returns(dbSetMock.Object);

        // Act
        var result = await _repository.GetProduct(targetProduct.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(targetProduct.Id, result.Id);
        Assert.Equal(targetProduct.Name, result.Name);
    }

    [Fact]
    public async Task GetTotalCount_ShouldReturnCorrectCount()
    {
        // Arrange
        var dbSetMock = new Mock<DbSet<Product>>();
        dbSetMock.As<IQueryable<Product>>().Setup(m => m.Provider).Returns(_products.AsQueryable().Provider);
        dbSetMock.As<IQueryable<Product>>().Setup(m => m.Expression).Returns(_products.AsQueryable().Expression);
        dbSetMock.As<IQueryable<Product>>().Setup(m => m.ElementType).Returns(_products.AsQueryable().ElementType);
        dbSetMock.As<IQueryable<Product>>().Setup(m => m.GetEnumerator()).Returns(_products.GetEnumerator());
        
        _contextMock.Setup(x => x.Products).Returns(dbSetMock.Object);

        // Act
        var result = await _repository.GetTotalCount();

        // Assert
        Assert.Equal(_products.Count, result);
    }

    [Fact]
    public async Task UpdateDescription_ShouldCallSaveChanges()
    {
        // Arrange
        var productId = Guid.NewGuid();
        var newDescription = "Updated Description";
        var dbSetMock = new Mock<DbSet<Product>>();
        
        _contextMock.Setup(x => x.Products).Returns(dbSetMock.Object);
        _contextMock.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(1));

        // Act
        await _repository.UpdateDescription(productId, newDescription);

        // Assert
        _contextMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}
