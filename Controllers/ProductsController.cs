using Microsoft.AspNetCore.Mvc;
using MongoApi.Entities;
using MongoApi.Repository;

namespace MongoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var products = await _productRepository.GetAllAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            return product is not null ? Ok(product) : NotFound();
        }

        [HttpGet("GetByPriceRange/{minPrice}/{maxPrice}")]
        public async Task<IActionResult> GetByPriceRange(decimal minPrice, decimal maxPrice)
        {
            var products = await _productRepository.GetByPriceRange(minPrice, maxPrice);
            return Ok(products);
        }

        [HttpPost]
        public async Task<ActionResult> Create(Product product)
        {
            await _productRepository.CreateAsync(product);
            return CreatedAtAction(nameof(Get), new { id = product.Id }, product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, Product product)
        {
            await _productRepository.UpdateAsync(id, product);
            return Ok(product);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _productRepository.DeleteAsync(id);
            return Ok(new { Sucess = true, Message = "Product deleted" });
        }
    }
}
