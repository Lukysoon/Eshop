using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductManager.Models.Dto;
using ProductManager.Repositories;
using ProductManager.Services;

namespace ProductManager.Controllers.V2
{
    [Route("api")]
    [Route("api/v{version:apiVersion}")]
    [ApiVersion("2.0")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [MapToApiVersion("2.0")]
        [Route("products/{pageIndex}/{pageSize}")]
        public async Task<IActionResult> GetPaginatedProducts([FromRoute] int pageIndex, [FromRoute]int pageSize)
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

        [HttpGet]
        [MapToApiVersion("2.0")]
        [Route("product/{id}")]
        public async Task<IActionResult> GetProduct(Guid id)
        {
            try
            {
                ProductDto product = await _productService.GetProduct(id);

                return Ok(product);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);                
            }
        }

        [HttpPatch]
        [MapToApiVersion("2.0")]
        [Route("product/update/description")]
        public async Task<IActionResult> UpdateProductDescription([FromQuery] Guid id,[FromQuery] string description)
        {
            try
            {
                await _productService.UpdateDescription(id, description);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);                
            }
        }
    }
}
