using CasgemMicroservices.Catalog.DTOs.ProductDTOs;
using CasgemMicroservices.Catalog.Services.ProductServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CasgemMicroservices.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> ProductList()
        {
            var value = await _productService.GetProductListAsync();
            return Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(CreateProductDTO createProductDTO)
        {
            await _productService.CreateProductAsync(createProductDTO);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            await _productService.DeleteProductAsync(id);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct(UpdateProductDTO updateProductDTO)
        {
            await _productService.UpdateProductAsync(updateProductDTO);
            return Ok();
        }

        [HttpGet("GetByID")]
        public async Task<IActionResult> GetProductById(string id)
        {
            var value = await _productService.GetProductByIdAsync(id);
            return Ok(value);
        }
    }
}
