using AutoMapper;
using Moq;
using ProductManager.Entities;
using ProductManager.Exceptions;
using ProductManager.Models.Dto;
using ProductManager.Repositories;
using ProductManager.Services;
using ProductManager.Tests.Data;

namespace ProductManager.Tests.Services;

public class ProductServiceTests
{
    private readonly Mock<IProductRepository> _mockRepository;
    private readonly Mock<IMapper> _mockMapper;
    private readonly ProductService _service;
    private readonly List<Product> _testProducts;
    private readonly List<ProductDto> _testProductDtos;

    public ProductServiceTests()
    {
        _mockRepository = new Mock<IProductRepository>();
        _mockMapper = new Mock<IMapper>();
        _service = new ProductService(_mockRepository.Object, _mockMapper.Object);

        _testProducts = ProductData.GetProducts();
        _testProductDtos = ProductData.GetProductDtos();
    }

    [Fact]
    public async Task GetPaginatedProducts_ShouldReturnMappedProducts()
    {
        int pageIndex = 2;
        int pageSize = 2;
        
        var repositoryResult = _testProducts.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        var mapperResult = _testProductDtos.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

        _mockRepository.Setup(r => r.GetPaginatedProducts(pageIndex, pageSize))
            .ReturnsAsync(repositoryResult);
        _mockMapper.Setup(m => m.Map<List<ProductDto>>(It.IsAny<List<Product>>()))
            .Returns(mapperResult);

        var result = await _service.GetPaginatedProducts(pageIndex, pageSize);

        Assert.Equal(pageSize, result.Count);
        Assert.Contains(_testProductDtos[2].Id, result.Select(r => r.Id));
        Assert.Contains(_testProductDtos[3].Id, result.Select(r => r.Id));
    }

    [Fact]
    public async Task GetAllProducts_ShouldReturnMappedProducts()
    {
        _mockRepository.Setup(r => r.GetAllProducts())
            .ReturnsAsync(_testProducts);
        _mockMapper.Setup(m => m.Map<List<ProductDto>>(It.IsAny<List<Product>>()))
            .Returns(_testProductDtos);

        var result = await _service.GetAllProducts();

        Assert.Equal(_testProductDtos.Count, result.Count);
        Assert.Contains(_testProductDtos[0].Id, result.Select(r => r.Id));
        Assert.Contains(_testProductDtos[_testProductDtos.Count - 1].Id, result.Select(r => r.Id));
    }

    [Fact]
    public async Task GetTotalPagesCount_ShouldReturnCorrectCount()
    {
        int pageSize = 2;
        int totalProductCount = 4;

        _mockRepository.Setup(r => r.GetTotalCount())
            .ReturnsAsync(totalProductCount);

        var result = await _service.GetTotalPagesCount(pageSize);

        Assert.Equal(2, result); // Ceiling of 4/2 = 2 pages
    }

    [Fact]
    public async Task GetProduct_ShouldReturnMappedProduct()
    {
        var productId = _testProducts[0].Id;
        _mockRepository.Setup(r => r.GetProduct(productId))
            .ReturnsAsync(_testProducts[0]);
        _mockMapper.Setup(m => m.Map<ProductDto>(It.IsAny<Product>()))
            .Returns(_testProductDtos[0]);

        var result = await _service.GetProduct(productId);

        Assert.Equal(_testProductDtos[0].Id, result.Id);
        Assert.Equal(_testProductDtos[0].Name, result.Name);
        Assert.Equal(_testProductDtos[0].Description, result.Description);
    }

    [Fact]
    public async Task GetProduct_WithInvalidId_ShouldThrowNotFoundException()
    {
        var invalidId = Guid.NewGuid();
        _mockRepository.Setup(r => r.GetProduct(invalidId))
            .ReturnsAsync((Product?)null);

        await Assert.ThrowsAsync<NotFoundException>(() => 
            _service.GetProduct(invalidId));
    }

    [Fact]
    public async Task UpdateDescription_ShouldCallRepository()
    {
        var productId = Guid.NewGuid();
        var description = "New Description";

        await _service.UpdateDescription(productId, description);

        _mockRepository.Verify(r => r.UpdateDescription(productId, description), Times.Once);
    }
}
