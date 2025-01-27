using Microsoft.AspNetCore.Mvc;
using Moq;
using ProductManager.Controllers;
using ProductManager.Exceptions;
using ProductManager.Models.Dto;
using ProductManager.Services;

namespace ProductManager.Tests.Controllers;

public class ProductControllerTests
{
    private readonly Mock<IProductService> _serviceMock;
    private readonly ProductController _controller;
    private readonly List<ProductDto> _productDtos;

    public ProductControllerTests()
    {
        _serviceMock = new Mock<IProductService>();
        _controller = new ProductController(_serviceMock.Object);

        // Setup test data
        _productDtos = new List<ProductDto>
        {
            new ProductDto { Id = Guid.NewGuid(), Name = "Product 1", Description = "Description 1", Price = 10.99m },
            new ProductDto { Id = Guid.NewGuid(), Name = "Product 2", Description = "Description 2", Price = 20.99m },
            new ProductDto { Id = Guid.NewGuid(), Name = "Product 3", Description = "Description 3", Price = 30.99m }
        };
    }

    [Fact]
    public async Task GetPaginatedProducts_ReturnsOkResult_WithPaginatedList()
    {
        // Arrange
        int pageIndex = 1;
        int pageSize = 2;
        int totalPages = 2;
        var paginatedProducts = _productDtos.Take(pageSize).ToList();

        _serviceMock.Setup(x => x.GetPaginatedProducts(pageIndex, pageSize))
            .ReturnsAsync(paginatedProducts);
        _serviceMock.Setup(x => x.GetTotalPagesCount(pageIndex, pageSize))
            .ReturnsAsync(totalPages);

        // Act
        var result = await _controller.GetPaginatedProducts(pageIndex, pageSize);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var paginatedList = Assert.IsType<PaginatedList<ProductDto>>(okResult.Value);
        Assert.Equal(pageSize, paginatedList.Items.Count);
        Assert.Equal(pageIndex, paginatedList.PageIndex);
        Assert.Equal(totalPages, paginatedList.TotalPages);
    }

    [Fact]
    public async Task GetPaginatedProducts_WhenExceptionOccurs_ReturnsInternalServerError()
    {
        // Arrange
        _serviceMock.Setup(x => x.GetPaginatedProducts(It.IsAny<int>(), It.IsAny<int>()))
            .ThrowsAsync(new Exception("Test exception"));

        // Act
        var result = await _controller.GetPaginatedProducts(1, 10);

        // Assert
        var statusCodeResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(500, statusCodeResult.StatusCode);
    }

    [Fact]
    public async Task GetAllProducts_ReturnsOkResult_WithAllProducts()
    {
        // Arrange
        _serviceMock.Setup(x => x.GetAllProducts())
            .ReturnsAsync(_productDtos);

        // Act
        var result = await _controller.GetAllProducts();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var products = Assert.IsType<List<ProductDto>>(okResult.Value);
        Assert.Equal(_productDtos.Count, products.Count);
    }

    [Fact]
    public async Task GetAllProducts_WhenExceptionOccurs_ReturnsInternalServerError()
    {
        // Arrange
        _serviceMock.Setup(x => x.GetAllProducts())
            .ThrowsAsync(new Exception("Test exception"));

        // Act
        var result = await _controller.GetAllProducts();

        // Assert
        var statusCodeResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(500, statusCodeResult.StatusCode);
    }

    [Fact]
    public async Task GetProduct_WhenExists_ReturnsOkResult()
    {
        // Arrange
        var productDto = _productDtos[0];
        _serviceMock.Setup(x => x.GetProduct(productDto.Id))
            .ReturnsAsync(productDto);

        // Act
        var result = await _controller.GetProduct(productDto.Id);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedProduct = Assert.IsType<ProductDto>(okResult.Value);
        Assert.Equal(productDto.Id, returnedProduct.Id);
    }

    [Fact]
    public async Task GetProduct_WhenNotFound_ReturnsInternalServerError()
    {
        // Arrange
        var nonExistentId = Guid.NewGuid();
        _serviceMock.Setup(x => x.GetProduct(nonExistentId))
            .ThrowsAsync(new NotFoundException($"Product with ID {nonExistentId} not found."));

        // Act
        var result = await _controller.GetProduct(nonExistentId);

        // Assert
        var statusCodeResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(500, statusCodeResult.StatusCode);
    }

    [Fact]
    public async Task UpdateProductDescription_WhenSuccessful_ReturnsOkResult()
    {
        // Arrange
        var productId = Guid.NewGuid();
        var newDescription = "Updated Description";

        // Act
        var result = await _controller.UpdateProductDescription(productId, newDescription);

        // Assert
        Assert.IsType<OkResult>(result);
        _serviceMock.Verify(x => x.UpdateDescription(productId, newDescription), Times.Once);
    }

    [Fact]
    public async Task UpdateProductDescription_WhenExceptionOccurs_ReturnsInternalServerError()
    {
        // Arrange
        _serviceMock.Setup(x => x.UpdateDescription(It.IsAny<Guid>(), It.IsAny<string>()))
            .ThrowsAsync(new Exception("Test exception"));

        // Act
        var result = await _controller.UpdateProductDescription(Guid.NewGuid(), "test");

        // Assert
        var statusCodeResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(500, statusCodeResult.StatusCode);
    }
}
