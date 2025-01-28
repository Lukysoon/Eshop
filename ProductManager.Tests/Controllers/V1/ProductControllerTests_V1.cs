using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ProductManager.Controllers;
using ProductManager.Controllers.V1;
using ProductManager.Exceptions;
using ProductManager.Models.Dto;
using ProductManager.Services;
using ProductManager.Tests.Data;

namespace ProductManager.Tests.Controllers.V2;

public class ProductControllerTests_V1
{
    private readonly Mock<IProductService> _mockService;
    private readonly ProductController _controller;
    private readonly List<ProductDto> _testProductDtos;

    public ProductControllerTests_V1()
    {
        _mockService = new Mock<IProductService>();
        _controller = new ProductController(_mockService.Object);

        _testProductDtos = ProductData.GetProductDtos();
    }

    [Fact]
    public async Task GetAllProducts_ShouldReturnOkWithProducts()
    {
        _mockService.Setup(s => s.GetAllProducts())
            .ReturnsAsync(_testProductDtos);

        var result = await _controller.GetAllProducts();

        var okResult = Assert.IsType<OkObjectResult>(result);
        var products = Assert.IsType<List<ProductDto>>(okResult.Value);
        Assert.Equal(_testProductDtos.Count, products.Count);
    }

    [Fact]
    public async Task GetAllProducts_WhenExceptionOccurs_ShouldReturn500()
    {
        _mockService.Setup(s => s.GetAllProducts())
            .ThrowsAsync(new Exception("Test exception"));

        var result = await _controller.GetAllProducts();

        var statusCodeResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(StatusCodes.Status500InternalServerError, statusCodeResult.StatusCode);
    }

    [Fact]
    public async Task GetProduct_ShouldReturnOkWithProduct()
    {
        var productId = _testProductDtos[0].Id;
        _mockService.Setup(s => s.GetProduct(productId))
            .ReturnsAsync(_testProductDtos[0]);

        var result = await _controller.GetProduct(productId);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var product = Assert.IsType<ProductDto>(okResult.Value);
        Assert.Equal(_testProductDtos[0].Id, product.Id);
    }

    [Fact]
    public async Task GetProduct_WhenExceptionOccurs_ShouldReturn500()
    {
        // Arrange
        var productId = Guid.NewGuid();
        _mockService.Setup(s => s.GetProduct(productId))
            .ThrowsAsync(new Exception("Test exception"));

        // Act
        var result = await _controller.GetProduct(productId);

        // Assert
        var statusCodeResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(StatusCodes.Status500InternalServerError, statusCodeResult.StatusCode);
    }

    [Fact]
    public async Task UpdateProductDescription_ShouldReturnOk()
    {
        // Arrange
        var productId = Guid.NewGuid();
        var description = "New Description";

        // Act
        var result = await _controller.UpdateProductDescription(productId, description);

        // Assert
        Assert.IsType<OkResult>(result);
        _mockService.Verify(s => s.UpdateDescription(productId, description), Times.Once);
    }

    [Fact]
    public async Task UpdateProductDescription_WhenExceptionOccurs_ShouldReturn500()
    {
        // Arrange
        var productId = Guid.NewGuid();
        var description = "New Description";
        _mockService.Setup(s => s.UpdateDescription(productId, description))
            .ThrowsAsync(new Exception("Test exception"));

        // Act
        var result = await _controller.UpdateProductDescription(productId, description);

        // Assert
        var statusCodeResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(StatusCodes.Status500InternalServerError, statusCodeResult.StatusCode);
    }
}
