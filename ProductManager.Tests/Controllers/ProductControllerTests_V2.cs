using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ProductManager.Controllers.V2;
using ProductManager.Models.Dto;
using ProductManager.Services;
using ProductManager.Tests.Data;

namespace ProductManager.Tests.Controllers;

public class ProductControllerTests_v2
{
    private readonly Mock<IProductService> _mockService;
    private readonly ProductController _controller;
    private readonly List<ProductDto> _testProductDtos;

    public ProductControllerTests_v2()
    {
        _mockService = new Mock<IProductService>();
        _controller = new ProductController(_mockService.Object);

        _testProductDtos = ProductData.GetProductDtos();
    }

    [Fact]
    public async Task GetPaginatedProducts_ShouldReturnOkWithPaginatedList()
    {
        int pageIndex = 2;
        int pageSize = 2;
        int totalPages = 2;
        var expectedResult = _testProductDtos.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

        _mockService.Setup(s => s.GetPaginatedProducts(pageIndex, pageSize))
            .ReturnsAsync(expectedResult);
        _mockService.Setup(s => s.GetTotalPagesCount(pageSize))
            .ReturnsAsync(totalPages);

        var result = await _controller.GetPaginatedProducts(pageIndex, pageSize);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var paginatedList = Assert.IsType<PaginatedList<ProductDto>>(okResult.Value);
        Assert.Equal(expectedResult.Count, paginatedList.Items.Count);
        Assert.Equal(pageIndex, paginatedList.PageIndex);
        Assert.Equal(totalPages, paginatedList.TotalPages);
    }
}
