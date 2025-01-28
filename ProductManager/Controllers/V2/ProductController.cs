using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductManager.Models.Dto;
using ProductManager.Repositories;
using ProductManager.Services;

namespace ProductManager.Controllers.V2
{
    [ApiController]
    [Route("api")]
    [Route("api/v{version:apiVersion}")]
    [ApiVersion("2.0")]
    public class ProductController2 : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController2(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [MapToApiVersion("2.0")]
        [Route("products/{pageIndex}/{pageSize}")]
        public async Task<IActionResult> GetPaginatedProducts2([FromRoute] int pageIndex, [FromRoute]int pageSize)
        {
            try
            {
                List<ProductDto> products = await _productService.GetPaginatedProducts(pageIndex, pageSize);

                int totalPages = await _productService.GetTotalPagesCount(pageSize);
                
                return Ok(new PaginatedList<ProductDto>(products, pageIndex, totalPages));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);                
            }
        }
    }
}
