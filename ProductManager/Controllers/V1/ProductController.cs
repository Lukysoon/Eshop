using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductManager.Models.Dto;
using ProductManager.Repositories;
using ProductManager.Services;

namespace ProductManager.Controllers.V1
{
    [Route("api")]
    [Route("api/v{version:apiVersion}")]
    [ApiVersion("1.0")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        
        [MapToApiVersion("1.0")]
        [HttpGet]
        [Route("products")]
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                List<ProductDto> products = await _productService.GetAllProducts();

                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);                
            }
        }

        [MapToApiVersion("1.0")]
        [HttpGet]
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

        [MapToApiVersion("1.0")]
        [HttpPatch]
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
