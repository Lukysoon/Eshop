using AutoMapper;
using Moq;
using ProductManager.Entities;
using ProductManager.Exceptions;
using ProductManager.Models.Dto;
using ProductManager.Repositories;
using ProductManager.Services;

namespace ProductManager.Tests.Services;

public class ProductServiceTests
{
    private readonly Mock<IProductRepository> _repositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly ProductService _service;
    private readonly List<Product> _products;
    private readonly List<ProductDto> _productDtos;

    public ProductServiceTests()
    {
        _repositoryMock = new Mock<IProductRepository>();
        _mapperMock = new Mock<IMapper>();
        _service = new ProductService(_repositoryMock.Object, _mapperMock.Object);

        // Setup test data
        _products = new List<Product>
        {
            new Product { Id = Guid.NewGuid(), Name = "Product 1", Description = "Description 1", Price = 10.99m },
            new Product { Id = Guid.NewGuid(), Name = "Product 2", Description = "Description 2", Price = 20.99m },
            new Product { Id = Guid.NewGuid(), Name = "Product 3", Description = "Description 3", Price = 30.99m }
        };

        _productDtos = _products.Select(p => new ProductDto 
        { 
            Id = p.Id, 
            Name = p.Name, 
            Description = p.Description, 
            Price = p.Price 
        }).ToList();
    }

    [Fact]
    public async Task GetPaginatedProducts_ShouldReturnMappedDtos()
    {
        // Arrange
        int pageIndex = 1;
        int pageSize = 2;
        _repositoryMock.Setup(x => x.GetPaginatedProducts(pageIndex, pageSize))
            .ReturnsAsync(_products.Take(pageSize).ToList());
        _mapperMock.Setup(x => x.Map<List<ProductDto>>(It.IsAny<List<Product>>()))
            .Returns(_productDtos.Take(pageSize).ToList());

        // Act
        var result = await _service.GetPaginatedProducts(pageIndex, pageSize);

        // Assert
        Assert.Equal(pageSize, result.Count);
        _repositoryMock.Verify(x => x.GetPaginatedProducts(pageIndex, pageSize), Times.Once);
        _mapperMock.Verify(x => x.Map<List<ProductDto>>(It.IsAny<List<Product>>()), Times.Once);
    }

    [Fact]
    public async Task GetAllProducts_ShouldReturnAllMappedDtos()
    {
        // Arrange
        _repositoryMock.Setup(x => x.GetAllProducts())
            .ReturnsAsync(_products);
        _mapperMock.Setup(x => x.Map<List<ProductDto>>(It.IsAny<List<Product>>()))
            .Returns(_productDtos);

        // Act
        var result = await _service.GetAllProducts();

        // Assert
        Assert.Equal(_productDtos.Count, result.Count);
        _repositoryMock.Verify(x => x.GetAllProducts(), Times.Once);
        _mapperMock.Verify(x => x.Map<List<ProductDto>>(It.IsAny<List<Product>>()), Times.Once);
    }

    [Fact]
    public async Task GetTotalPagesCount_ShouldCalculateCorrectly()
    {
        // Arrange
        int totalCount = 10;
        int pageSize = 3;
        int expectedPages = 4; // Ceiling of 10/3
        _repositoryMock.Setup(x => x.GetTotalCount())
            .ReturnsAsync(totalCount);

        // Act
        var result = await _service.GetTotalPagesCount(1, pageSize);

        // Assert
        Assert.Equal(expectedPages, result);
        _repositoryMock.Verify(x => x.GetTotalCount(), Times.Once);
    }

    [Fact]
    public async Task GetProduct_WhenExists_ShouldReturnMappedDto()
    {
        // Arrange
        var product = _products[0];
        var productDto = _productDtos[0];
        _repositoryMock.Setup(x => x.GetProduct(product.Id))
            .ReturnsAsync(product);
        _mapperMock.Setup(x => x.Map<ProductDto>(product))
            .Returns(productDto);

        // Act
        var result = await _service.GetProduct(product.Id);

        // Assert
        Assert.Equal(productDto.Id, result.Id);
        Assert.Equal(productDto.Name, result.Name);
        _repositoryMock.Verify(x => x.GetProduct(product.Id), Times.Once);
        _mapperMock.Verify(x => x.Map<ProductDto>(product), Times.Once);
    }

    [Fact]
    public async Task GetProduct_WhenNotExists_ShouldThrowNotFoundException()
    {
        // Arrange
        var nonExistentId = Guid.NewGuid();
        _repositoryMock.Setup(x => x.GetProduct(nonExistentId))
            .ReturnsAsync((Product?)null);

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() => 
            _service.GetProduct(nonExistentId));
    }

    [Fact]
    public async Task UpdateDescription_ShouldCallRepository()
    {
        // Arrange
        var productId = Guid.NewGuid();
        var newDescription = "Updated Description";

        // Act
        await _service.UpdateDescription(productId, newDescription);

        // Assert
        _repositoryMock.Verify(x => x.UpdateDescription(productId, newDescription), Times.Once);
    }
}
